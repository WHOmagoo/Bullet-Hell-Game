PROJECT_DIR=/home/christian/Documents/school/2019/spring/cs487/project/playground
mgcb \
--workingDir:$PROJECT_DIR \
--compress:True \
--importer:TextureImporter \
--processor:TextureProcessor \
--outputDir=$PROJECT_DIR/Content \
--platform=DesktopGL \
--build:$1