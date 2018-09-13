@echo off

set /p directory="Build directory (%localappdata%\osu!): "
IF "%directory%" == "" set directory=%localappdata%\osu!
set /p version="Version (1.0.0 beta): "
IF "%version%" == "" set version=1.0.0 beta

set standard=%directory%\Skins\poyosu! (Standard)
set lite=%directory%\Skins\poyosu! (Lite)
set output=%directory%\Exports

:: Delete preexisting skin folders.
IF EXIST "%standard%" rmdir /s /q "%standard%"
mkdir "%standard%"
IF EXIST "%lite%" rmdir /s /q "%lite%"
mkdir "%lite%"

echo Building shared assets...
pushd .\shared
for /r %%a in (*.png, *.wav, *.mp3) do (
	COPY /y "%%a" "%standard%\%%~nxa" > nul
	COPY /y "%%a" "%lite%\%%~nxa" > nul
)
popd

echo Building standard-only assets...
pushd .\standard
for /r %%a in (*.png, *.wav, *.mp3, *.ini) do (
	COPY /y "%%a" "%standard%\%%~nxa" > nul
)
popd

echo Building lite-only assets...
pushd .\lite
for /r %%a in (*.png, *.wav, *.mp3, *.ini) do (
	COPY /y "%%a" "%lite%\%%~nxa" > nul
)
popd

echo Building .osk files...
:: 7zip is very noisy. Please be quiet.
"C:\Program Files\7-Zip\7z.exe" a "%output%\standard.zip" "%standard%\*" > nul
"C:\Program Files\7-Zip\7z.exe" a "%output%\lite.zip" "%lite%\*" > nul
del "%output%\poyosu! (Standard %version%).osk" 2>nul
del "%output%\poyosu! (Lite %version%).osk" 2>nul
ren "%output%\standard.zip" "poyosu! (Standard %version%).osk"
ren "%output%\lite.zip" "poyosu! (Lite %version%).osk"
echo Done!