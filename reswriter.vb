Imports System
Imports System.Resources
Imports System.Windows.Forms

Public Class WriteResources
    
    Public Sub New()
        messagebox.Show("WriteResources") 
        ' Creates a resource writer.
        Dim writer As New ResourceWriter("GreetingResources.resources")
        
        'http://msdn.microsoft.com/en-us/library/stf461k5(v=vs.110).aspx
        ' Adds resources to the resource writer.
        writer.AddResource("prompt", "Enter your name:")
        
        writer.AddResource("greeting", "Hello, {0}!")
        
        writer.AddResource("dog", system.drawing.image.fromfile("dog.png"))
        
        ' Writes the resources to the file or stream, and closes it.
        writer.Close()           
        
    End sub
        
    Public Shared Sub Main()
        dim wr as new WriteResources
    End Sub
    
End Class
