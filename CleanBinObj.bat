@echo off
setlocal enabledelayedexpansion

REM ´Delete "bin" and "obj" Folders in current directory
if exist bin rmdir /s /q bin
if exist obj rmdir /s /q obj

REM DSearch all sub folders and delete "bin" and "obj" directories
for /d %%d in (*) do (
    if exist "%%d\bin" (
        echo Deleting %%d\bin
        rmdir /s /q "%%d\bin"
    )
    if exist "%%d\obj" (
        echo Deleting %%d\obj
        rmdir /s /q "%%d\obj"
    )
)

echo Done.
pause