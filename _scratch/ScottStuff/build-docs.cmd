@ECHO OFF
IF /i [%1] == [local] GOTO :local
IF /i [%1] == [incremental] GOTO :incremental
ECHO usage:
ECHO To build the Jekyll site and debug locally "build-docs local"
ECHO To build the Jekyll site and debug locally (faster build) "build-docs incremental"
GOTO :end

:local
cls
ECHO [Building docs and serving them locally]
ECHO [Wait until the localhost url is ready (it might take a while), then copy the url and visit it with the browser]
call bundle exec jekyll serve --watch
GOTO :end

:incremental
cls
ECHO [Building docs and serving them locally]
ECHO [Wait until the localhost url is ready (it might take a while), then copy the url and visit it with the browser]
call bundle exec jekyll serve --watch --incremental
GOTO :end


:end
