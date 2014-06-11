@ECHO OFF

set z_path="c:\GitHub\DPS_Diablo3\ILMerge\7z.exe"
set target_path=%2
set target_file=%~nx2
set target_dir=%~dp2


echo %target_path%
echo %target_file%
echo %target_dir%
echo %z_path%

pause

rem #    set output path and result file path
set result="C:\Users\dolenin\Dropbox\Public\DPS_Diablo3.7z"

rem #    run merge cmd

echo "%z_path%" a %result%" "%target_path%"
%z_path% a %result% %target_path%

pause

rem #    if succeded
IF %ErrorLevel% EQU 0 (
    
        @echo Result: %target_file% "->  %target_path%"
       @echo Result: %target_file% "->  %result%" 
   
   set status=succeded
   set errlvl=0    
) ELSE (
    set status=failed 
    set errlvl=1
    )

@echo Merge %status%
exit %errlvl% 

REM copy_publ "c:\GitHub\DPS_Diablo3\" "c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\DPS_Diablo3.exe"