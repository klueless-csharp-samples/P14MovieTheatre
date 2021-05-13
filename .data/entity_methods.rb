module EntityMethods
  attr_accessor :current_entity

  def entities
    KUtil.data.to_open_struct(@entities)
  end

  def filtered(take = 999)
    # entities.take().select { |m| m[:type] == :model }.take(take)
    entities.take(take)
  end
  

  def raw_entities
    @entities
  end

  def apply_aliases
    # Apply Aliases
    @entities.each do |entity|
      # Apply alias
      entity[:name] = entity[:model_name]
      
      # Mappers
      entity[:column].each do |c|
        c[:csharp_type] = map_csharp_type(c[:type])
      end
      # Apply alias
      entity[:columns] = entity[:column]
      entity[:scopes] = entity[:scope]
      massage_belongs_to(entity[:belongs_to])
    end
  end

  def map_csharp_type(type)
    case type
    when :string
      'string'
    when :text
      'string' # NEED TO DEAL WITH THIS BETTER
    when :integer
      'int'
    when :boolean
      'bool'
    when :datetime
      'DateTime'
    when :jsonb
      'object'
    else
      'string'
    end
  end
  

  def entity(**opts, &block)
    @current_entity = {
      scope: [],
      column: [],
      foreign: [],
      belongs_to: [],
      has_one: [],
      has_many: [],
      public_instance_methods: [],
      private_instance_methods: [],
      public_class_methods: [],
      statistics: {}
    }.merge(opts)

    @entities << @current_entity

    instance_eval(&block) if block_given?
  end

  def scope(name, **opts)
    current_entity[:scope] << { name: name }.merge(opts)
  end

  def column(name, **opts)
    current_entity[:column] << { name: name }.merge(opts)
  end

  def foreign(name, **opts)
    current_entity[:foreign] << { name: name }.merge(opts)
  end

  def belongs_to(name, **opts)
    current_entity[:belongs_to] << { name: name }.merge(opts)
  end

  def has_one(name, **opts)
    current_entity[:has_one] << { name: name }.merge(opts)
  end

  def has_many(name, **opts)
    current_entity[:has_many] << { name: name }.merge(opts)
  end

  def public_instance_methods(*values)
    current_entity[:public_instance_methods] = values.to_a
  end

  def private_instance_methods(*values)
    current_entity[:private_instance_methods] = values.to_a
  end

  def public_class_methods(*values)
    current_entity[:public_class_methods] = values.to_a
  end


  def statistics(&block)
    instance_eval(&block) if block_given?
  end
  
  def code_counts(**opts)
    current_entity[:statistics][:code_counts] = opts
  end
  def code_dsl_counts(**opts)
    current_entity[:statistics][:code_dsl_counts] = opts
  end
  def column_counts(**opts)
    current_entity[:statistics][:column_counts] = opts
  end
  def row_counts(**opts)
    current_entity[:statistics][:row_counts] = opts
  end
  def issues(*issues)
    current_entity[:statistics][:issues] = issues
  end

  def massage_belongs_to(belongs)
    belongs.each do |belong|
      belong[:generate_belongs_options] = generate_belongs_options(belong)
    end
  end

  def generate_belongs_options(belong)
    key_values = belong.keys.select { |key| key != :name }.map do |key|
      "#{key}: #{belong[key]}"
    end

    key_values.length > 0 ? ", #{key_values.join(', ')}" : ''
  end
    
  def debug
    log.kv 'Entities', @entities.length

    @entities.each do |entity|
      log.section_heading(entity[:model_name])
      kv_if 'Scopes', entity[:scope].count 
      kv_if 'Columns', entity[:column].count 
      kv_if 'Foreign columns', entity[:foreign].count
      kv_if 'Belong to columns', entity[:belongs_to].count

      kv_if 'Public instance methods' , entity[:public_instance_methods].count
      kv_if 'Private instance methods' , entity[:private_instance_methods].count
      kv_if 'Public class methods' , entity[:public_class_methods].count

      log.pretty_params(entity[:statistics])
    

      # log.open_struct(entity)
      # puts entity
    end
  end

  def kv_if(name, value)
    log.kv name, value if value > 0
  end
end
