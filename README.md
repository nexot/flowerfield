Flowerfield
===========
Playground for visual basic.


Flowerfield.vb
---------------
Playground for tests


Miner.vb
----------------
Simple minesweeper



Requirements
-----------------
.net v.2 at least


Compile and run
-----------------
Compile
    
    %windir%\Microsoft.NET\Framework\v2.0.50727\vbc.exe flowerfield.vb
    
Then run flowerfield.exe


Other
----------------
    
npp (/w nppexec) script
    
      NPE_CONSOLE o0 i0
      del "$(NAME_PART)".exe
      $(SYS.WINDIR)\Microsoft.NET\Framework\v2.0.50727\vbc.exe "$(FILE_NAME)"
      NPP_RUN "$(NAME_PART)".exe
      NPE_CONSOLE o1 i1
