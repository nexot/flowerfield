Imports System.Windows.Forms
Imports System.Collections.CollectionBase 'used in buttonarray
Imports System.Resources 'image load
Imports System.Timers




Public Class Test

    Inherits System.Windows.Forms.Form
    
    dim Xsize as integer = 20
    dim Ysize as integer = 20
    
    dim fsize as integer = (Xsize+1)*(Ysize+1) 'field size
    
    dim cleanBurrons as Boolean = True   
    dim timestopped as boolean = true
    dim mousepressed as boolean = false
    dim myevent as MouseEventArgs
       
    dim minetotals as integer = 20   
       
    private b as New Button()
    private c as New Button()
    private d as New Button()
    
    Dim Label1 as New Label()
    Dim Label2 as New Label()
    
    Private Shared aTimer As System.Timers.Timer
    Private _elapseStartTime As DateTime
    Dim elapsedtime as System.TimeSpan
    
    'Dim MyControlArray as ButtonArray
    
    public mybuttarray(Xsize,Ysize) as button
     
    
    Public Sub New()
    
        MyBase.New()
        MyBase.Topmost = True
        MyBase.Text = "Это Заголовок формы: Игра в сапера на минном поле"
        MyBase.Size = new System.Drawing.Size(520,520)
        
       ' MyControlArray = New ButtonArray(Me)
       ' MyControlArray.AddNewButton()
       ' MyControlArray(0).BackColor = System.Drawing.Color.Red
        
        b.Dock = DockStyle.Top
        b.Text = "Это Кнопка b: Создать пустое поле/Удалить поле"
        addhandler b.Click, addressof b_click    

        c.Dock = DockStyle.Top
        c.Text = "Это Кнопка c: Добавить немного мин"
        addhandler c.Click, addressof c_click         

        d.Dock = DockStyle.Top
        d.Text = "Это Кнопка d: Остановить/Запустить таймер"
        addhandler d.Click, addressof d_click      
                

        Label1.BackColor = System.Drawing.Color.LightGray
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        'Label1.Width = 60
        Label1.Text = "Это метка Label1: Таймер"
        'Label1.location = new System.Drawing.Point(20, 77)
        Label1.Dock = DockStyle.Top
    
    
        Label2.BackColor = System.Drawing.Color.DarkGray
        Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Label2.Text = "Это метка Label2: Статус игры"
        Label2.Dock = DockStyle.Top    
    
    
        MyBase.Controls.Add(Label2)
        MyBase.Controls.Add(Label1)
 
 
        MyBase.Controls.Add(d)
        MyBase.Controls.Add(c)
        MyBase.Controls.Add(b)

 
        call settimer
        call gobuttons
        
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
        
        _elapseStartTime = DateTime.Now
  
    end sub

    

    
    public sub gobuttons
    dim i,j as integer
        
    for i = 0 to Xsize
    
        for j = 0 to Ysize
 
            if cleanBurrons then ' when new buttons needed

                console.write (". Nothing, i=" & CStr(i) & ";j=" & CStr(j))
                mybuttarray(i,j) = new button()
                mybuttarray(i,j).location = new System.Drawing.Point(20 + i*16, 130 + j*16)
                mybuttarray(i,j).Name = ""                
                mybuttarray(i,j).Tag = New Integer() {i, j, 0, 0, 0} 'mine, clicked, flagged
                mybuttarray(i,j).Size = new System.Drawing.Size(16,16)
                mybuttarray(i,j).Capture = false
                mybuttarray(i,j).FlatStyle = FlatStyle.Standard
                MyBase.Controls.Add(mybuttarray(i,j))
                addhandler mybuttarray(i,j).MouseDown, addressof butt_clicker 
                
          
            else  ' clear buttons here
                
                console.write ("Dispose from button {0}, {1} ; " , i ,j)
                MyBase.Controls.Remove(mybuttarray(i,j))
                mybuttarray(i,j).Tag = New Integer() {i, j, -1, -1 ,-1}
                mybuttarray(i,j).Dispose()
                console.write ("what's left is {0} ; " , mybuttarray(i,j).Tag(2))
                
            end if
              
            
                             
        next j
    
    next i
    
    cleanBurrons = not cleanBurrons 'flip state
    
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
                
                mybuttarray(i,j).Tag = thisplace
                mineset = mineset + 1     
                
                console.writeline("mine {0} set at {1}, {2}",mineset,i,j)
            end if                
        'next t    
        loop
    
    end sub

   
    
    Public Sub butt_clicker(ByVal sender As Object, ByVal e As MouseEventArgs)
        
        if timestopped then 
            timestopped = false
            aTimer.Enabled = True
        end if

        
        dim zis as button
        zis = CType(sender, System.Windows.Forms.Button)
          
        
        
        console.writeline("clicked button i = {0}, j = {1}, mineflag = {2}",zis.Tag(0), zis.Tag(1),zis.Tag(2))
        
        console.writeline("button = {0}", e.Button)
        
        select case e.Button 
            case MouseButtons.Right
                if zis.tag(3) = 1 then exit select ' do nothing on open field
                
                console.writeline("do = {0}", "Flag")
                
                if zis.tag(4) = 2 then zis.tag(4) = 0 else zis.tag(4) += 1 
                ' circle states (0=empty)->(1=flag)->(2=question mark)
                
                console.writeline("set flag to {0}", zis.tag(4))
                
                select case cint(zis.tag(4))
                    case 0
                        zis.Image = Nothing
                    case 1
                        zis.Image = system.drawing.image.fromfile("smile.png")
                    case 2
                        zis.Image = system.drawing.image.fromfile("cat.png")
                        
                end select
                
            case MouseButtons.Left
                if not zis.tag(4) = 0 then exit select ' do nothing on flagged field
                if not zis.tag(3) = 0 then exit select ' do nothing on open field
                
                console.writeline("do = {0}", "Check")
         
                if zis.tag(2) = 1 then 
                    console.writeline ("BANG BANG !")
                    zis.tag(3) = 1
                    zis.FlatStyle = FlatStyle.Popup
                    zis.Image = system.drawing.image.fromfile("dog.png")
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
                
                'check game end = open all minesfree place
                
                dim s,r as integer
                dim chks as integer = fsize
                dim bangs as integer = 0
                for s = 0 to Xsize
                    for r = 0 to Ysize
                        
                        chks -= mybuttarray(s,r).tag(2) + mybuttarray(s,r).tag(3) - mybuttarray(s,r).tag(2) * mybuttarray(s,r).tag(3) 'open or mined
                        bangs += mybuttarray(s,r).tag(2) * mybuttarray(s,r).tag(3) ' open and mined
                        
                    next r
                next s
                
                console.writeline("left to free = {0}, banged = {1}", chks, bangs)
                Label1.Text= String.Format("Left to disarm = {0}, life spent = {1}", chks, bangs)
                
                if chks=0 then 
                    console.writeline("Victory! Life spent = {0}", bangs)
                    elapsedtime = DateTime.Now.Subtract(_elapseStartTime)
                    console.writeline(String.Format("{0}hr : {1}min : {2}sec", elapsedtime.Hours, elapsedtime.Minutes, elapsedtime.Seconds))
                    

                    Label1.Text= String.Format("Victory!, life spent = {0}",  bangs)
                    Label2.Text= String.Format("{0} hr : {1} min : {2} sec : {3} ms", elapsedtime.Hours, elapsedtime.Minutes, elapsedtime.Seconds, elapsedtime.Milliseconds)
                    
                    
                    timestopped = true
                    aTimer.Enabled = false

                    
                end if
                
            case MouseButtons.Middle
                console.writeline("do = {0}", "Auto")
                
                dim i,j as integer
                for i = math.max(0, zis.tag(0)-1) to math.min(Xsize, zis.tag(0)+1) 
                    for j = math.max(0, zis.tag(1)-1) to math.min(Ysize, zis.tag(1)+1) 
                        if mybuttarray(i,j).tag(3)=0 and mybuttarray(i,j).tag(4)=0 then
                            console.writeline("autoclick on i = {0}, j = {1}",i,j)
                            call butt_clicker(mybuttarray(i,j), New MouseEventArgs(Windows.Forms.MouseButtons.Left, 1, 0, 0, 0))                            
                        end if
                    next j
                next i
                
                
                
        end select

        

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
        elapsedtime = DateTime.Now.Subtract(_elapseStartTime)
        Label2.Text= String.Format("{0} hr : {1} min : {2} sec : {3} ms", elapsedtime.Hours, elapsedtime.Minutes, elapsedtime.Seconds, elapsedtime.Milliseconds)
        
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
