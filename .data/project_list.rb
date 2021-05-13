class ProjectList
  class << self
    def current
      @current ||= ProjectList.new
    end

    def debug
      current.debug
    end
  end

  def all
    project_list
  end
  
  def active
    @active ||= project_list.select { |p| p[:a] == 1 }.map { |p| KUtil.data.to_open_struct(p) }
  end
  
  def project_list
    return @project_list if defined? @project_list

    app_settings = AppSettings.current

    # valid_types: :library, :console, :webapp, :webapi
    
    @project_list = [
      { a: 1 }.merge(app_settings.for_project(:movie_theatre)),
    ]

    @project_list.each do |project|
      project[:variant] = nil unless project.key?(:variant)
    end

    @project_list
  rescue => exception
    binding.pry
  end

  def debug
    log.section_heading 'Project list'
    tp project_list, :name, :application, :database, :application_lib_path, :application_namespace
  end
end
