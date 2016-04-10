@ECHO OFF

REM  Load Visual Studio Developer Command Prompt with path variable
call "%~f1..\Tools\vsvars32.bat"

REM Disassemble the DLL file into intermediate language code
ildasm %~f2packages\MouseKeyHook.5.4.0\lib\net40\Gma.System.MouseKeyHook.dll /out:%~f2packages\MouseKeyHook.5.4.0\lib\net40\Gma.System.MouseKeyHook.il

REM Reassemble the IL code into a signed DLL file
ilasm %~f2packages\MouseKeyHook.5.4.0\lib\net40\Gma.System.MouseKeyHook.il /dll /key=%~f2ISEPresenter.snk /output=%~f2packages\MouseKeyHook.5.4.0\lib\net40\Gma.System.MouseKeyHook.dll
