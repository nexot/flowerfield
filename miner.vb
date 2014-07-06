Imports System.Windows.Forms
Imports System.Collections.CollectionBase 'used in buttonarray
Imports System.Resources 'image load
Imports System.Timers




Public Class Test

    Inherits System.Windows.Forms.Form
    
    dim Xsize as integer = 20
    dim Ysize as integer = 20
    dim cleanBurrons as Boolean = True   
    dim timestopped as boolean = true
    dim mousepressed as boolean = false
    dim myevent as MouseEventArgs
       
    dim minetotals as integer = 20   
       
    private b as New Button()
    private c as New Button()
    private d as New Button()
    
    Private Shared aTimer As System.Timers.Timer
    
    'Dim MyControlArray as ButtonArray
    
    public mybuttarray(Xsize,Ysize) as button
     
    
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

        c.Dock = DockStyle.Top
        c.Text = "Это Кнопка c"
        addhandler c.Click, addressof c_click         

        d.Dock = DockStyle.Top
        d.Text = "Это Кнопка d"
        addhandler d.Click, addressof d_click      
        
        
        MyBase.Controls.Add(d)
        MyBase.Controls.Add(c)
        MyBase.Controls.Add(b)
        
        
        MyBase.Show()                                
            
        'b.PerformClick()  
        
            
    End sub


    public sub settimer
        ' Create a timer with a ten second interval.
        aTimer = New System.Timers.Timer(10000)

        ' Hook up the Elapsed event for the timer. 
        AddHandler aTimer.Elapsed, AddressOf OnTimedEvent

        ' Set the Interval to 2 seconds (2000 milliseconds).
        aTimer.Interval = 1000
        
        aTimer.SynchronizingObject = me
  
    end sub

    

    
    public sub gobuttons
    dim i,j as integer
        
    for i = 0 to Xsize
    
        for j = 0 to Ysize

            'mybuttarray(i,j) = Nothing
            
            if cleanBurrons then 

                console.write (". Nothing, i=" & CStr(i) & ";j=" & CStr(j))
                mybuttarray(i,j) = new button()
                mybuttarray(i,j).location = new System.Drawing.Point(20 + i*16, 70 + j*16)
                mybuttarray(i,j).Name = ""                
                mybuttarray(i,j).Tag = New Integer() {i, j, 0, 0, 0} 'mine, clicked, flagged
                mybuttarray(i,j).Size = new System.Drawing.Size(16,16)
                
                MyBase.Controls.Add(mybuttarray(i,j))
                addhandler mybuttarray(i,j).MouseDown, addressof butt_clicker 
                'addhandler mybuttarray(i,j).MouseUp, addressof butt_up
                'addhandler mybuttarray(i,j).MouseEnter, addressof butt_enter
                
            else
                
                console.write ("Dispose from button {0}, {1} ; " , i ,j)
                MyBase.Controls.Remove(mybuttarray(i,j))
                mybuttarray(i,j).Tag = New Integer() {i, j, -1, -1 ,-1}
                mybuttarray(i,j).Dispose()
                console.write ("what's left is {0} ; " , mybuttarray(i,j).Tag(2))
            end if
              
            mybuttarray(i,j).FlatStyle = FlatStyle.System
                             
        next j
    
    next i
    
    cleanBurrons = not cleanBurrons
    end sub

   
    public function randomValue(byref lowerbound as integer,byref upperbound as integer) as integer
        randomValue = CInt(Math.Floor((upperbound - lowerbound + 1) * Rnd())) + lowerbound
    end function    


    
    public sub setmines
    
        dim i,j,mineset as integer
        dim thisplace as integer()
        mineset = 0
        
        do while mineset < minetotals
        'for t = 1 to 10
        
            j = randomvalue(0,Ysize)
            i = randomvalue(0,Xsize)
            console.writeline("mineset {0}, minetotals {1}, i = {2}, j = {3}",mineset,minetotals, i ,j)
            thisplace = mybuttarray(i,j).Tag
            console.writeline("tag 0 = {0}, 1 = {1}, 2 = {2}",thisplace(0),thisplace(1),thisplace(2))
            if thisplace(2) = 0 then
                'set mine here             
                thisplace(2) = 1 
                mybuttarray(i,j).Image = system.drawing.image.fromfile("dog.png")
                mybuttarray(i,j).Tag = thisplace
                mineset = mineset + 1     
                
                console.writeline("mine {0} set at {1}, {2}",mineset,i,j)
            end if                
        'next t    
        loop
    
    end sub

   
    
    Public Sub butt_clicker(ByVal sender As Object, ByVal e As MouseEventArgs)
         
        dim zis as button
        zis = CType(sender, System.Windows.Forms.Button)
          
        zis.Capture = false
        
        console.writeline("clicked button i = {0}, j = {1}, mineflag = {2}",zis.Tag(0), zis.Tag(1),zis.Tag(2))
        
        if zis.tag(2) = 1 then 
            console.writeline ("BANG BANG !")
            zis.FlatStyle = FlatStyle.Popup
        else
            
            dim i,j,localmines as integer
            localmines = 0
            for i = math.max(0, zis.tag(0)-1) to math.min(Xsize, zis.tag(0)+1) 
                for j = math.max(0, zis.tag(1)-1) to math.min(Ysize, zis.tag(1)+1) 
                    if mybuttarray(i,j).tag(2) = 1 then localmines += 1
                next j
            next i
        
            zis.text = cstr(localmines)
            zis.FlatStyle = FlatStyle.Popup
			zis.tag(3) = 1
        
            if localmines = 0 then 
                for i = math.max(0, zis.tag(0)-1) to math.min(Xsize, zis.tag(0)+1) 
                    for j = math.max(0, zis.tag(1)-1) to math.min(Ysize, zis.tag(1)+1) 
                        if mybuttarray(i,j).tag(3)=0 then
                            console.writeline("autoclick on i = {0}, j = {1}",i,j)
                            call butt_clicker(mybuttarray(i,j), New MouseEventArgs(Windows.Forms.MouseButtons.Left, 1, 0, 0, 0))                            
                        end if
                    next j
                next i
            end if
        
        end if
       
        console.writeline("mouse down!")   
    

    end sub

    
    public sub b_click(ByVal sender as object, byval e as eventargs)
        console.writeline("b_click executed" & vbCrlf & "on object : " & sender.tostring() & vbCrlf & vbTab  & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
       ' MyControlArray.Remove()
       if not timestopped then d.PerformClick()
       
       call gobuttons
       call settimer
    end sub
    
    public sub c_click(ByVal sender as object, byval e as eventargs)
       console.writeline("c_click executed" & vbCrlf & "on object : " & sender.tostring() & vbCrlf & vbtab  & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))       

        call setmines
    end sub
    
    public sub d_click(ByVal sender as object, byval e as eventargs)
       console.writeline("d_click executed" & vbCrlf & "on object : " & sender.tostring() & vbCrlf & vbtab  & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))       
       timestopped = not timestopped
       aTimer.Enabled = not aTimer.Enabled
    end sub    
    
    
    ' Specify what you want to happen when the Elapsed event is  
    ' raised. 
    
    public Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)
        Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime)
        'call dogmove
    End Sub 
    
   
    
    Public Shared Sub Main()
        'Dim T as New Test
        'T = New Test()
        System.Windows.Forms.Application.Run(New Test())
    End Sub
    
End Class


''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
