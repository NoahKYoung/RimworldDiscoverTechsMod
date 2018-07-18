TITLE Update Files From Source
ECHO OFF
CLS
ECHO ========== Setting path variables. ==========
SET ProjectDir=..\RimworldDiscoverTechsMod\Sources\RimworldDiscoverTechs\
ECHO ------ GitHub mod root directory: ------
ECHO %ProjectDir%
SET GithubDir=..\RimworldDiscoverTechsMod\
ECHO ------ GitHub mod root directory: ------
ECHO %GithubDir%
SET GithubReleaseDir=%GithubDir%RELEASE\TechnologyBlueprints
ECHO ------ GitHub mod release directory: ------
ECHO %GithubReleaseDir%
SET RimWorldDir=\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\TechnologyBlueprints
ECHO ------ RimWorld mod directory: ------
ECHO %RimWorldDir%
ECHO ========== Done setting path variables. ==========
PAUSE
CLS
ECHO ========== Copying README to Github mod root directory. ==========
XCOPY "%ProjectDir%README.md" "%GithubDir%\README.md" /D /Y
ECHO ========== Copied README to Github mod root directory. ==========
PAUSE
CLS
ECHO ========== Checking folders for GitHub mod root directory. ==========
ECHO ------ Checking "About" folder ------
XCOPY "%ProjectDir%About" "%GithubDir%About" /D /S /E /Y
ECHO ------ Checking "Defs" folder ------
XCOPY "%ProjectDir%Defs" "%GithubDir%Defs" /D /S /E /Y
ECHO ------ Checking "Patches" folder ------
XCOPY "%ProjectDir%Patches" "%GithubDir%Patches" /D /S /E /Y
ECHO ------ Checking "Textures" folder ------
XCOPY "%ProjectDir%Textures" "%GithubDir%Textures" /D /S /E /Y
ECHO ========== Done checking folders for GitHub mod root directory. =======
PAUSE
CLS
ECHO ========== Checking folders for GitHub release directory. ==========
ECHO ------ Checking "About" folder ------
XCOPY "%GithubDir%About" "%GithubReleaseDir%\About" /D /S /E /Y
ECHO ------ Checking "Assemblies" folder ------
XCOPY "%GithubDir%Assemblies" "%GithubReleaseDir%\Assemblies" /D /S /E /Y
ECHO ------ Checking "Defs" folder ------
XCOPY "%GithubDir%Defs" "%GithubReleaseDir%\Defs" /D /S /E /Y
ECHO ------ Checking "Patches" folder ------
XCOPY "%GithubDir%Patches" "%GithubReleaseDir%\Patches" /D /S /E /Y
ECHO ------ Checking "Textures" folder ------
XCOPY "%GithubDir%Textures" "%GithubReleaseDir%\Textures" /D /S /E /Y
ECHO ========== Done checking folders for GitHub release directory. ==========
PAUSE
CLS
ECHO ========== Copying release directory to RimWorld mod directory. ==========
XCOPY "%GithubReleaseDir%" "\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\TechnologyBlueprints" /D /S /E /Y
ECHO ========== Copied release directory to RimWorld mod directory. ==========
PAUSE
CLS
ECHO All done!
PAUSE