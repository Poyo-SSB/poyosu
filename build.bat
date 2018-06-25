@echo off

set /p directory="Build directory (%localappdata%\osu!): "
IF "%directory%" == "" set directory=%localappdata%\osu!

set standard=%directory%\Skins\poyosu! (Standard)
set lite=%directory%\Skins\poyosu! (Lite)
set classic=%directory%\Skins\poyosu! (Classic)
set output=%directory%\Exports

:: Delete preexisting skin folders.
IF EXIST "%standard%" rmdir /s /q "%standard%"
mkdir "%standard%"
IF EXIST "%lite%" rmdir /s /q "%lite%"
mkdir "%lite%"
IF EXIST "%classic%" rmdir /s /q "%classic%"
mkdir "%classic%"

echo Building shared assets...
pushd .\shared
for /r %%a in (*.png, *.wav, *.mp3) do (
	COPY /y "%%a" "%standard%\%%~nxa" > nul
	COPY /y "%%a" "%lite%\%%~nxa" > nul
	COPY /y "%%a" "%classic%\%%~nxa" > nul
)
popd

echo Building standard+lite assets...
pushd .\standard+lite
for /r %%a in (*.png, *.wav, *.mp3, *.ini) do (
	COPY /y "%%a" "%standard%\%%~nxa" > nul
	COPY /y "%%a" "%lite%\%%~nxa" > nul
)
popd

echo Building standard+classic assets...
pushd .\standard+classic
for /r %%a in (*.png, *.wav, *.mp3, *.ini) do (
	COPY /y "%%a" "%standard%\%%~nxa" > nul
	COPY /y "%%a" "%classic%\%%~nxa" > nul
)
popd

echo Building lite+classic assets...
pushd .\lite+classic
for /r %%a in (*.png, *.wav, *.mp3, *.ini) do (
	COPY /y "%%a" "%lite%\%%~nxa" > nul
	COPY /y "%%a" "%classic%\%%~nxa" > nul
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

echo Building classic-only assets...
pushd .\classic
for /r %%a in (*.png, *.wav, *.mp3, *.ini) do (
	COPY /y "%%a" "%classic%\%%~nxa" > nul
)
popd

echo Building .osk files...
:: 7zip is very noisy. Please be quiet.
"C:\Program Files\7-Zip\7z.exe" a "%output%\standard.zip" "%standard%\*" > nul
"C:\Program Files\7-Zip\7z.exe" a "%output%\lite.zip" "%lite%\*" > nul
"C:\Program Files\7-Zip\7z.exe" a "%classic%\classic.zip" "%classic%\*" > nul
del "%output%\poyosu! (Standard).osk" 2>nul
del "%output%\poyosu! (Lite).osk" 2>nul
del "%output%\poyosu! (Classic).osk" 2>nul
ren "%output%\standard.zip" "poyosu! (Standard).osk"
ren "%output%\lite.zip" "poyosu! (Lite).osk"
ren "%output%\classic.zip" "poyosu! (Classic).osk"
echo Done!