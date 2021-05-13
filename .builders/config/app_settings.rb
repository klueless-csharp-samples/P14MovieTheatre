class AppSettings
  class << self
    def current
      @current ||= AppSettings.new
    end

    def debug
      current.debug
    end
  end

  def initialize
    @project_settings = {}

    init
  end

  def solution
    @solution ||= KUtil.data.to_open_struct(base)
  end

  def project(key)
    return @project_settings[key] if @project_settings.key?(key)

    log.error "Project settings not found for key: #{key}"

    return {}
  end
  alias :for_project :project

  def debug
    log.section_heading('base')
    log.kv_hash(base)

    @project_settings.keys.each do |key|
      log.section_heading(@project_settings[key][:name])
      log.kv_hash(@project_settings[key])
    end
  end

  private

  # Convert all of this into some type of DSL
  def init
    project = 'ClubMembership.Web'
    settings = base.merge({
      name:                          :movie_theatre,
      project_type:                  :web,
      project_variant:               :entity_framework,
      app_type:                      'MVC App',
      application:                   project,
      application_lib_path:          project,
      application_namespaces:        ['MovieTheatre'],
      app_path:                      File.join(base[:app_path], project),
    })
    add_to_project_and_massage(settings)
  end
  
  def base
    return @base if defined? @base
    @base = begin
      value = {
        name:                          :movie_theatre,
        description:                   'MovieTheatre in C#',
        application:                   'MovieTheatre',
        database:                      'P14',
        database_password:             'Hambaro3',
        git_repo_name:                 'MovieTheatre',
        git_organization:              'klueless-csharp-samples',
        avatar:                        'Cinema Owner',
        main_story:                    'As Cinema Owner, I want to manage session times and tickets, so that I can have a profitable club',
        author:                        'David Cruwys',
        author_email:                  'david@ideasmen.com.au',
        copyright_date:                '2021',
        website:                       'http://appydave.com/csharp/samples/club_membership',
        application_lib_path:          'P14MovieTheatre',
        application_namespaces:        ['P14MovieTheatre'],
        template_rel_path:             'csharp-mvc',
        app_path:                      '~/dev/csharp/P14ClubMembership',
        data_path:                     '_/.data'
      }
      massage_settings(value)
    end      
  end

  def add_to_project_and_massage(settings)
    add_to_project(massage_settings(settings))
  end

  def massage_settings(settings)
    settings[:application_namespace] = settings[:application_namespaces].join('.')
    settings[:project_dotnet_type] = map_dotnet_type(settings[:project_type])
    settings
  end

  def add_to_project(settings)
    key = settings[:name]
    @project_settings[key] = settings
  end

  def map_dotnet_type(project_type)
    case project_type
    when :library
      :classlib
    when :test
      :xunit
    else
      project_type
    end
  end

  # Templates                                         Short Name          Language          Tags
  # --------------------------------------------      --------------      ------------      ----------------------
  # Console Application                               console             [C#], F#, VB      Common/Console
  # Class library                                     classlib            [C#], F#, VB      Common/Library
  # Worker Service                                    worker              [C#], F#          Common/Worker/Web
  # Unit Test Project                                 mstest              [C#], F#, VB      Test/MSTest
  # NUnit 3 Test Project                              nunit               [C#], F#, VB      Test/NUnit
  # NUnit 3 Test Item                                 nunit-test          [C#], F#, VB      Test/NUnit
  # xUnit Test Project                                xunit               [C#], F#, VB      Test/xUnit
  # Razor Component                                   razorcomponent      [C#]              Web/ASP.NET
  # Razor Page                                        page                [C#]              Web/ASP.NET
  # MVC ViewImports                                   viewimports         [C#]              Web/ASP.NET
  # MVC ViewStart                                     viewstart           [C#]              Web/ASP.NET
  # Blazor Server App                                 blazorserver        [C#]              Web/Blazor
  # Blazor WebAssembly App                            blazorwasm          [C#]              Web/Blazor/WebAssembly
  # ASP.NET Core Empty                                web                 [C#], F#          Web/Empty
  # ASP.NET Core Web App (Model-View-Controller)      mvc                 [C#], F#          Web/MVC
  # ASP.NET Core Web App                              webapp              [C#]              Web/MVC/Razor Pages
  # ASP.NET Core with Angular                         angular             [C#]              Web/MVC/SPA
  # ASP.NET Core with React.js                        react               [C#]              Web/MVC/SPA
  # ASP.NET Core with React.js and Redux              reactredux          [C#]              Web/MVC/SPA
  # Razor Class Library                               razorclasslib       [C#]              Web/Razor/Library
  # ASP.NET Core Web API                              webapi              [C#], F#          Web/WebAPI
  # ASP.NET Core gRPC Service                         grpc                [C#]              Web/gRPC
  # dotnet gitignore file                             gitignore                             Config
  # global.json file                                  globaljson                            Config
  # NuGet Config                                      nugetconfig                           Config
  # Dotnet local tool manifest file                   tool-manifest                         Config
  # Web Config                                        webconfig                             Config
  # Solution File                                     sln                                   Solution
  # Protocol Buffer File                              proto                                 Web/gRPC

end

