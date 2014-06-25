Flowerfield
===========

Simple game using visual basic.net.


Compile and run on system with .net. v2 installed. To compile run
    
    C:\Windows\Microsoft.NET\Framework\v2.0.50727\vbc.exe flowerfield.vb
    
It generates flowerfield.exe file to run.

===========
    
At npp_exec this script compiles and runs the project:
    
      NPE_CONSOLE o0 i0
      del "$(NAME_PART)".exe
      C:\Windows\Microsoft.NET\Framework\v2.0.50727\vbc.exe "$(FILE_NAME)"
      NPP_RUN "$(NAME_PART)".exe
      NPE_CONSOLE o1 i1
