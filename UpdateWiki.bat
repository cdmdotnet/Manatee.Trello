set arg1=%1

cd Wiki
git pull
cd ..

git clone https://github.com/gregsdennis/AutoWiki.git
cd AutoWiki\AutoWiki
git pull

dotnet run -p AutoWiki.csproj ..\..\Wiki

if "%1" NEQ "-local" (
    echo "-local not found"
    exit /B
    cd ..\..\Wiki
    git add -A
    git commit -m "updated after build"
    git push
)

cd ..\..

rd /s /q AutoWiki