SET PATH=g:\bin\texLive\bin\win32;G:\bin\Graphviz\bin\;%PATH%
SET PATH=C:\bin\texLive\bin\win32;G:\bin\Graphviz2008\bin\;%PATH%
G:\bin\doxygen.1.8.9.1\doxygen.exe report.doxygen
cd ./bld/html
G:\agp\bin\help\hhc.exe  index.hhp 
cd ..
cd ..
cp   ./bld/html/report.chm .

