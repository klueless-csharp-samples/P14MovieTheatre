class EntitiesAll
  include EntityMethods

  def initialize
    @entities = []

    definition

    apply_aliases
  end

  def definition
    entity model_name: :movie,
           model_name_plural: :movies do
      column      :title         , type: :string    
    end

    entity model_name: :session,
           model_name_plural: :sessions do
      column      :movie_id      , type: :integer
      column      :time          , type: :datetime   
    end

    entity model_name: :ticket,
           model_name_plural: :tickets do
      column      :session_id      , type: :integer
      column      :name          , type: :string   
    end
  end
end
