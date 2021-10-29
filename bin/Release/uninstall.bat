echo off
cls
REG DELETE HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run /v "SimpleHeartBeatService" /f
echo "The uninstall has finished."