TITLE Update Files From Source
ECHO OFF
CLS
ECHO ========== Setting path variables. ==========
SET ProjectDir=..\RimworldDiscoverTechsMod\Sources\RimworldDiscoverTechs\
ECHO ------ GitHub mod root directory: ------
ECHO %ProjectDir%
SET GithubDir=..\RimworldDiscoverTechsMod
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
XCOPY /d /y "%ProjectDir%README.md" "%GithubDir%\README.md"
ECHO ========== Copied README to Github mod root directory. ==========
PAUSE
CLS
ECHO ========== Checking folders for GitHub mod root directory. ==========
ECHO ------ Checking "About" folder ------
XCOPY /d  /s /e /y "%ProjectDir%About" "%GithubDir%About"
ECHO ------ Checking "Defs" folder ------
XCOPY /d  /s /e /y "%ProjectDir%Defs" "%GithubDir%Defs"
ECHO ------ Checking "Patches" folder ------
XCOPY /d  /s /e /y "%ProjectDir%Patches" "%GithubDir%Patches"
ECHO ------ Checking "Textures" folder ------
XCOPY /d  /s /e /y "%ProjectDir%Textures" "%GithubDir%Textures"
ECHO ========== Done checking folders for GitHub mod root directory. =======
PAUSE
CLS
ECHO ========== Checking folders for GitHub release directory. ==========
ECHO ------ Checking "About" folder ------
XCOPY /d  /s /e /y "%GithubDir%About" "%GithubReleaseDir%\About"
ECHO ------ Checking "Assemblies" folder ------
XCOPY /d  /s /e /y "%GithubDir%Assemblies" "%GithubReleaseDir%\Assemblies"
ECHO ------ Checking "Defs" folder ------
XCOPY /d  /s /e /y "%GithubDir%Defs" "%GithubReleaseDir%\Defs"
ECHO ------ Checking "Patches" folder ------
XCOPY /d  /s /e /y "%GithubDir%Patches" "%GithubReleaseDir%\Patches"
ECHO ------ Checking "Textures" folder ------
XCOPY /d  /s /e /y "%GithubDir%Textures" "%GithubReleaseDir%\Textures"
ECHO ========== Done checking folders for GitHub release directory. ==========
PAUSE
CLS
ECHO ========== Copying release directory to RimWorld mod directory. ==========
XCOPY /d /s /e /y "%GithubReleaseDir%" "\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\TechnologyBlueprints"
ECHO ========== Copied release directory to RimWorld mod directory. ==========
PAUSE
CLS
ECHO All done!
PAUSE