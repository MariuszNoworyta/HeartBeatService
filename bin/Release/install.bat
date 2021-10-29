echo off
cls
REG ADD HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run /v "SimpleHeartBeatService" /t REG_SZ /d %cd%\SimpleHeartBeatService.exe
echo "The install has finished."