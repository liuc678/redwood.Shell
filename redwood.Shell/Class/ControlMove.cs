using System.Drawing;
using System.Windows.Forms;

namespace redwood.Shell
{
    public class ControlMove
    {
        public ControlMove(Control ctl)
        {
            this.Control = ctl;
            Control.MouseDown += Panel_MouseDown;
            // 为Panel添加鼠标移动事件处理程序
            Control.MouseMove += Panel_MouseMove;
            // 为Panel添加鼠标释放事件处理程序
            Control.MouseUp += Panel_MouseUp;
        }

        public System.Windows.Forms.Control Control;


        private Point _dragStartPoint; // 用于存储鼠标按下时的Panel位置
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            // 当鼠标按下时，记录鼠标位置与Panel位置的偏移量
            _dragStartPoint = new Point(e.X, e.Y);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            // 如果鼠标左键被按下
            if (e.Button == MouseButtons.Left)
            {
                // 计算新的Panel位置
                Point newLocation = new Point(
                    Control.Left + (e.X - _dragStartPoint.X),
                    Control.Top + (e.Y - _dragStartPoint.Y)
                );
                // 设置Panel的新位置
                Control.Location = newLocation;
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            // 当鼠标释放时，重置偏移量
            _dragStartPoint = Point.Empty;
        }
    }
}
