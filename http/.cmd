@ECHO OFF
FOR /F "tokens=*" %%E IN (%~dp0.\.env) DO SET %%E
