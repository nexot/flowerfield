Imports System.Windows.Forms
Imports System.Collections.CollectionBase 'used in buttonarray
Imports System.Resources 'image load


Public Class Test
    Inherits System.Windows.Forms.Form
    
    dim Xsize as integer = 20
    dim Ysize as integer = 20
    
    private b as New Button()
    
    'Dim MyControlArray as ButtonArray
    
    public mybuttarray(Xsize,Ysize) as button
    
    Public Sub action()
        'MsgBox("Hello, World!")
        dim x,t as Integer
        x = 0
        for t = 1 to 10
            x = x + t
        next t
        'MsgBox(x)  
        console.writeline(x)
    End sub
    
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
        addhandler b.Click, addressof b_click             
                
        MyBase.Controls.Add(b)
        
        MyBase.Show()    
            
    End sub

    
    public sub gobuttons
    dim i,j as integer
    
    for i = 0 to Xsize
    
        for j = 0 to Ysize
            
            'mybuttarray(i,j) = Nothing
            
            if mybuttarray(i,j) is Nothing then 
                console.write (". Nothing, i=" & CStr(i) & ";j=" & CStr(j))
                mybuttarray(i,j) = new button()
                mybuttarray(i,j).location = new System.Drawing.Point(20 + i*16, 70 + j*16)
                mybuttarray(i,j).Name = ""
                mybuttarray(i,j).Tag = i*(Ysize+1)+j
                mybuttarray(i,j).Size = new System.Drawing.Size(16,16)
                
                MyBase.Controls.Add(mybuttarray(i,j))
                addhandler mybuttarray(i,j).MouseDown, addressof butt_clicker 
            else
                console.write (". i=" & CStr(i) & ";j=" & CStr(j))
            end if
            
            
            mybuttarray(i,j).FlatStyle = FlatStyle.Standard
            
            if (i-10)*(i-10)+(j-10)*(j-10)<57 then 
               if (i-10)*(i-10)+(j-10)*(j-10)>23 then 
                    mybuttarray(i,j).Image = system.drawing.image.fromfile("smile.png")
               else
                    mybuttarray(i,j).Image = system.drawing.image.fromfile("flower.png")
               end if
            else
                    mybuttarray(i,j).Image = system.drawing.image.fromfile("dot.png")            
            end if
            

            
        next j
    
    next i
    
    end sub
    
    Public Sub butt_clicker(ByVal sender As Object, ByVal e As _
        MouseEventArgs)
       'System.EventArgs)
       'MessageBox.Show("you have clicked button " & CType(CType(sender, _
        Console.Writeline("you have clicked button " & CType(CType(sender, _
          System.Windows.Forms.Button).Tag, String))
       dim zis as button
       zis = CType(sender, _
          System.Windows.Forms.Button)
       if zis.FlatStyle = FlatStyle.system then             
            zis.Image = system.drawing.image.fromfile("cat.png")
            zis.FlatStyle = FlatStyle.Standard
       elseif  zis.FlatStyle = FlatStyle.Flat then
            'zis.Image  = nothing       
            zis.FlatStyle = FlatStyle.system
       elseif  zis.Flatstyle = FlatStyle.Popup then
            zis.Flatstyle = FlatStyle.Flat
       else
            zis.Flatstyle = FlatStyle.Popup
       end if
          
    End Sub
    
    
    public sub b_click(ByVal sender as object, byval e as eventargs)
        call action
        console.writeline("b_click executed" & vbCrlf & "on object : " & sender.tostring())
       ' MyControlArray.Remove()
       call gobuttons
    end sub
    
    
    Public Shared Sub Main()
        'Dim T as New Test
        'T = New Test()
        System.Windows.Forms.Application.Run(New Test())
    End Sub
    
End Class




Public Class ButtonArray
   Inherits System.Collections.CollectionBase
   
   private HostForm as System.Windows.Forms.Form
   
   Public Function AddNewButton() As System.Windows.Forms.Button
       ' Create a new instance of the Button class.
       Dim aButton As New System.Windows.Forms.Button()
       ' Add the button to the collection's internal list.
       Me.List.Add(aButton)
       ' Add the button to the controls collection of the form 
       ' referenced by the HostForm field.
       HostForm.Controls.Add(aButton)
       ' Set intial properties for the button object.
       aButton.Top = Count * 25
       aButton.Left = 100
       aButton.Tag = Me.Count
       aButton.Text = "Button " & Me.Count.ToString
       
       AddHandler aButton.Click, AddressOf ClickHandler
       
       Return aButton
    End Function
   

    Public Sub New(ByVal host as System.Windows.Forms.Form)
       HostForm = host
       Me.AddNewButton()
    End Sub
   
   Default Public ReadOnly Property Item(ByVal Index As Integer) As _
       System.Windows.Forms.Button
       Get
          Return CType(Me.List.Item(Index), System.Windows.Forms.Button)
       End Get
    End Property
   
   Public Sub Remove()
   ' Check to be sure there is a button to remove.
       If Me.Count > 0 Then
          ' Remove the last button added to the array from the host form 
          ' controls collection. Note the use of the default property in 
          ' accessing the array.
          HostForm.Controls.Remove(Me(Me.Count -1))
          Me.List.RemoveAt(Me.Count -1)
       End If
    End Sub

    Public Sub ClickHandler(ByVal sender As Object, ByVal e As _
       System.EventArgs)
       MessageBox.Show("you have clicked button " & CType(CType(sender, _
          System.Windows.Forms.Button).Tag, String))
    End Sub
    
    
End Class
