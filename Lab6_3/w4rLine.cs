using System.Windows.Forms;

namespace Lab6_3
{
    class w4rLine : w4line
    {
        ToolBar tb = new ToolBar();
        p4sLine2 pan;

        public w4rLine(rLine l) : base(l)
        {
            rl = l;
            Padding = new Padding(10);
            AutoSize = true;
            StartPosition = FormStartPosition.CenterScreen;
            
            pan = new p4sLine2(rl);
            Controls.Add(pan);
            
            tb.ButtonSize = new System.Drawing.Size((int)(200 / 3), (int)(40));
            ToolBarButton Zoom_p = new ToolBarButton("Zoom +");
            ToolBarButton Zoom_m = new ToolBarButton("Zoom -");
            ToolBarButton Left = new ToolBarButton("Left");
            ToolBarButton Right = new ToolBarButton("Right");
            ToolBarButton Up = new ToolBarButton("Up");
            ToolBarButton Down = new ToolBarButton("Down");
            tb.Buttons.AddRange(new ToolBarButton[] {Zoom_p,Zoom_m,Left,Right,Up, Down });
            tb.Dock = DockStyle.Top;
            tb.ButtonClick += ToolBarButtonClick;
            Controls.Add(tb);
        }

        public void ToolBarButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Text == "Zoom +") // поменять параметры отображения
                pan.mp.zoom(-10);
            else if (e.Button.Text == "Zoom -")
                pan.mp.zoom(10);
            else if (e.Button.Text == "Right")
                pan.mp.moveEastWest(20);
            else if (e.Button.Text == "Left")
                pan.mp.moveEastWest(-20);
            else if (e.Button.Text == "Down")
                pan.mp.moveNordSouth(-20);
            else if (e.Button.Text == "Up")
                pan.mp.moveNordSouth(20);
            else
                return;
            
            pan.mkSLine(rl); // пересчитать линию экрана
            pan.Invalidate(); // вызвать событие Paint
        }
    }
}
