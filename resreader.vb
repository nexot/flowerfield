Imports System.Windows.Forms
Imports System.Resources
Imports System.Reflection
Imports System.IO


Public Class TestResources
    Inherits System.Windows.Forms.Form
    private b as New Button()
    
    Public Sub New()
        
        
        MyBase.New()
        MyBase.Topmost = True
        MyBase.Text = "Это Заголовок формы"
        MyBase.Size = new System.Drawing.Size(520,520)
        
       ' MyControlArray = New ButtonArray(Me)
       ' MyControlArray.AddNewButton()
       ' MyControlArray(0).BackColor = System.Drawing.Color.Red
        
        b.Dock = DockStyle.Top
        b.Text = "Это Кнопка b"

        
        MyBase.Controls.Add(b)        
        
        MyBase.Show()     
        
        ' http://msdn.microsoft.com/en-us/library/aa984408(v=vs.71).aspx
        Dim rm As New ResourceManager("GreetingResources", _
                                    GetType(TestResources).Assembly())
        Console.Write(rm.GetString("prompt"))
        
        
        b.Image = Ctype(rm.GetObject("dog"),System.Drawing.Image)
        
        
    End sub
        
    Public Shared Sub Main()
        System.Windows.Forms.Application.Run(New TestResources())
    End Sub
    
End Class
