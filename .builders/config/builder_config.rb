KBuilder.configure do |config|
  solution_root       = '~/dev/csharp/P14MovieTheatre'
  project_root       = '~/dev/csharp/P14MovieTheatre'
  templates_root    = '~/dev/kgems/k_dsl/_/.template'

  config.target_folders.add(:root         , solution_root)
  config.target_folders.add(:data         , solution_root, '.data')
  config.target_folders.add(:project      , project_root)

  # project.project_type        : web
  # project.project_dotnet_type : web
  # project.project_variant     : entity_framework

  # NOTE: The templates should really be related to the project type and variant,
  # e.g. there is no need for csharp-ef for anything but an entity framework project
  # General C#
  config.template_folders.add(:csharp     , File.join(templates_root, 'csharp'))

  # Entity framework
  config.template_folders.add(:csharp_ef  , File.join(templates_root, 'csharp-ef'))

  # Entity web application
  config.template_folders.add(:csharp_web , File.join(templates_root, 'csharp-web'))
end

def builder
  @builder ||= KBuilder::BaseBuilder.init
end
