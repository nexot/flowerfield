Flowerfield
===========

Simple game on visual basic.net.
.net required.


Compile and run
===========

Compile
    
    %windir%\Microsoft.NET\Framework\v2.0.50727\vbc.exe flowerfield.vb
    
Then run .exe


Other
===========
    
npp (/w nppexec) script
    
      NPE_CONSOLE o0 i0
      del "$(NAME_PART)".exe
      %windir%\Microsoft.NET\Framework\v2.0.50727\vbc.exe "$(FILE_NAME)"
      NPP_RUN "$(NAME_PART)".exe
      NPE_CONSOLE o1 i1
