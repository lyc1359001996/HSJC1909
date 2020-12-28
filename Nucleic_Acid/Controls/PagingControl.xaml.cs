using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfPaging
{
    /// <summary>
    /// 分页控件
    /// </summary>
    public partial class PagingControl : UserControl
    {
        #region 字段
        /// <summary>
        /// 总页数
        /// </summary>
        private int _pageTote = 1;
        /// <summary>
        /// 触发页面改变事件的参数
        /// </summary>
        private PagesChangedArgs _pagesChangedArgs;
        /// <summary>
        /// 滑动计时器
        /// </summary>
        private DispatcherTimer _scrollTimer;
        /// <summary>
        /// 滑动方向（True：右；False：左）
        /// </summary>
        private bool _scrollRight;
        /// <summary>
        /// 移动次数
        /// </summary>
        private int _scrollCount = 0;
        /// <summary>
        /// 每次滑动的数据
        /// </summary>
        private int offset = 0;
        /// <summary>
        /// 页码按钮尺寸
        /// </summary>
        private int _buttonSize = 40;
        /// <summary>
        /// 计时器间隔（毫秒）
        /// </summary>
        private int _timerInterval = 40;
        /// <summary>
        /// 是否可以触发当前页依赖属性的回调
        /// </summary>
        private bool _isCanTriggerCurrentPageDependencyCellback = true;
        #endregion

        #region 依赖属性

        #region 分页大小
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }
        /// <summary>
        /// 分页大小
        /// </summary>
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(PagingControl), new FrameworkPropertyMetadata(1, new PropertyChangedCallback(
                (obj, args) =>
                {
                    try
                    {
                        if (obj is PagingControl)
                        {
                            //计算总页数
                            ((PagingControl)obj).CalculatePageTote();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                })));
        #endregion

        #region 数据总数
        /// <summary>
        /// 数据总数
        /// </summary>
        public int DataTote
        {
            get { return (int)GetValue(DataToteProperty); }
            set { SetValue(DataToteProperty, value); Text_Total.Text = value.ToString(); }
        }
        /// <summary>
        /// 数据总数
        /// </summary>
        public static readonly DependencyProperty DataToteProperty =
            DependencyProperty.Register("DataTote", typeof(int), typeof(PagingControl), new FrameworkPropertyMetadata(1, new PropertyChangedCallback(
                (obj, args) =>
                {
                    try
                    {
                        if (obj is PagingControl)
                        {
                            //计算总页数
                            ((PagingControl)obj).CalculatePageTote();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                })));
        #endregion

        #region 当前页码
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }
        /// <summary>
        /// 当前页码
        /// </summary>
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(PagingControl), new FrameworkPropertyMetadata(1, new PropertyChangedCallback(
                (obj, args) =>
                {
                    try
                    {
                        if (obj is PagingControl && args.NewValue is int)
                        {
                            //判断是否可以执行以下代码
                            if (((PagingControl)obj)._isCanTriggerCurrentPageDependencyCellback == true)
                            {
                                //设置当前页码
                                ((PagingControl)obj).SetCurrentPage(Convert.ToInt32(args.NewValue));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                })));
        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 分页控件
        /// </summary>
        public PagingControl()
        {
            InitializeComponent();
            //初始化滑动计时器
            this._scrollTimer = new DispatcherTimer();
            this._scrollTimer.Tick += new EventHandler(this.ScrollTimer_Tick);
            this._scrollTimer.Interval = TimeSpan.FromMilliseconds(this._timerInterval);
        }
        #endregion

        #region 自定义事件
        /// <summary>
        /// 页码改变事件
        /// </summary>
        public event EventHandler<PagesChangedArgs> OnPagesChanged;
        #endregion

        #region 方法        

        #region 私有
        /// <summary>
        /// 计算总页数
        /// </summary>
        private void CalculatePageTote()
        {
            //判断分页大小和数据条数是否正确
            if (this.PageSize > 0 && this.DataTote > 0)
            {
                //计算余数，判断总页数是否需要加一
                double remainder = Convert.ToDouble(this.DataTote) % Convert.ToDouble(this.PageSize);
                //计算总页数
                this._pageTote = this.DataTote / this.PageSize;
                if (remainder > 0)
                {
                    this._pageTote = this._pageTote + 1;
                }

                //初始化分页控件
                this.InitializePageControl();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitializePageControl()
        {
            //重置可选的分页
            this.pagePanel.Children.Clear();

            //初始化各个分页按钮的状态（最大可选的只有10个页码）
            int showPageButtonCount = this._pageTote > 10 ? 10 : this._pageTote;
            for (int i = 1; i <= showPageButtonCount; i++)
            {
                //添加页码按钮
                this.AddPageButton(i, false);
            }

            //默认选中第一页
            this.SetCurrentPage(1);
        }
        /// <summary>
        /// 单击分页按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                //获取用户单击的分页按钮
                Button pageButton = sender as Button;
                if (pageButton.Tag is int)
                {
                    //获取分页按钮对应的页码，并设置当前页码
                    this.SetCurrentPage(Convert.ToInt32(pageButton.Tag));
                }
            }
        }
        /// <summary>
        /// 单击“上一页”按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackPage_Click(object sender, RoutedEventArgs e)
        {
            //计算上一页
            int page = this.CurrentPage - 1;
            if (page < 1)
            {
                page = 1;
            }
            //设置页码
            this.SetCurrentPage(page);
        }
        /// <summary>
        /// 单击“下一页”按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            //计算下一页
            int page = this.CurrentPage + 1;
            if (page > this._pageTote)
            {
                page = this._pageTote;
            }
            //设置页码
            this.SetCurrentPage(page);
        }
        /// <summary>
        /// 设置当前页码
        /// </summary>
        /// <param name="newPage">页码</param>
        private void SetCurrentPage(int newPage)
        {
            if (newPage > 0 && newPage <= this._pageTote)
            {
                //记录旧的页码
                int oldPage = this.CurrentPage;
                //设置新的页码（由于当前页“CurrentPage”是依赖属性，这里给它复制，不需要出发依赖属性的回调，所以加上了以下的状态标识）
                this._isCanTriggerCurrentPageDependencyCellback = false;
                this.CurrentPage = newPage;
                this._isCanTriggerCurrentPageDependencyCellback = true;
                //选中的已经是起始页，则“上一页”按钮不可用，否则可用
                if (this.CurrentPage == 1)
                {
                    this.btnBackPage.IsEnabled = false;
                }
                else
                {
                    this.btnBackPage.IsEnabled = true;
                }
                //选中的已经是末页，则“下一页”按钮不可用，否则可用
                if (this.CurrentPage == this._pageTote)
                {
                    this.btnNextPage.IsEnabled = false;
                }
                else
                {
                    this.btnNextPage.IsEnabled = true;
                }
                //创建页码改变参数
                this._pagesChangedArgs = new PagesChangedArgs()
                {
                    NewPage = newPage,
                    OldPage = oldPage,
                };
                //更新显示中的页码按钮
                this.UpdatePageButton();
                //设置分页按钮的样式
                this.SetPageButtonStyle();
                //抛出页码改变事件
                this.OnPagesChanged?.Invoke(this, this._pagesChangedArgs);
            }
        }
        /// <summary>
        /// 设置分页按钮控件的样式
        /// </summary>
        private void SetPageButtonStyle()
        {
            //遍历所有的分页按钮控件
            foreach (UIElement item in this.pagePanel.Children)
            {
                if (item is Button)
                {
                    //获取分页按钮控件
                    Button pageButton = item as Button;
                    //获取分页按钮对应的页码
                    int pageIndex = (pageButton.Tag is int) ? Convert.ToInt32(pageButton.Tag) : -1;

                    //判断当前按钮是否对应正在显示中的页面
                    if (pageIndex == this.CurrentPage)
                    {
                        //设置分页按钮选中状态的样式
                        pageButton.Background = new SolidColorBrush(Colors.LightBlue);
                    }
                    else
                    {
                        //设置分页按钮，默认状态的样式
                        pageButton.Background = new SolidColorBrush(Colors.White);
                    }
                }
            }
        }
        /// <summary>
        /// 更新页码按钮
        /// </summary>
        private void UpdatePageButton()
        {
            //没有发生页码选中改变，不需要执行此方法
            if (this._pagesChangedArgs == null || this._pagesChangedArgs.NewPage == this._pagesChangedArgs.OldPage)
            {
                return;
            }

            //新增按钮个数
            int addButtonCount = 0;

            //页码选择变大
            if (this._pagesChangedArgs.NewPage > this._pagesChangedArgs.OldPage)
            {
                //滚动条向右滑动
                this._scrollRight = true;

                //页码需要向后滑动
                if (this.CurrentPage < this._pageTote)
                {
                    //向后面添加页码按钮
                    int nowPage = this.CurrentPage + 1;
                    while (nowPage <= this.CurrentPage + 4 && nowPage <= this._pageTote)
                    {
                        //判断分页按钮是否已经显示
                        if (this.PageButtonIsShowing(nowPage) == false)
                        {
                            //添加页码按钮
                            this.AddPageButton(nowPage, false);
                            //记录新增按钮个数
                            addButtonCount++;
                        }
                        //累加页码序号
                        nowPage++;
                    }
                }
            }
            //页面选择变小
            else
            {
                //滚动条向左滑动
                this._scrollRight = false;

                //页码需要向前滑动
                if (this.CurrentPage > 1)
                {
                    //向前面添加页码按钮
                    int nowPage = this.CurrentPage - 1;
                    while (nowPage >= this.CurrentPage - 5 && nowPage >= 1)
                    {
                        //判断分页按钮是否已经显示
                        if (this.PageButtonIsShowing(nowPage) == false)
                        {
                            //添加页码按钮
                            this.AddPageButton(nowPage, true);
                            //记录新增按钮个数
                            addButtonCount++;
                        }
                        //递减页码序号
                        nowPage--;
                    }
                }
            }

            //计算平移参数（这里由于滚动条数据还没有变动，所以不能使用ScrollViewer.ScrollableWidth来做计算）
            this.offset = (this._buttonSize + 5) * addButtonCount / 4;
            //重置移动次数
            this._scrollCount = 0;
            if (offset > 0)
            {
                //开始平移页码按钮
                this._scrollTimer.Start();
            }
        }
        /// <summary>
        /// 添加页码按钮
        /// </summary>
        /// <param name="pageIndex">页码序号</param>
        /// <param name="insertIntoFirst">页码按钮插入位置（True：插入在最前面；False：插入在最后面）</param>
        private void AddPageButton(int pageIndex, bool insertIntoFirst)
        {
            //页码序号错误
            if (pageIndex < 1)
            {
                return;
            }
            //创建页码按钮
            Button pageButton = new Button()
            {
                Content = pageIndex,
                Tag = pageIndex,
                Height = this._buttonSize,
                Width = this._buttonSize,
                Margin = new Thickness(5, 0, 0, 0),
                Padding = new Thickness(-5 * (setPadding(pageIndex) - 1), 0, 0, 0)
            };
            pageButton.Click += PageButton_Click;
            //将页码按钮显示到界面上
            if (insertIntoFirst == true)
            {
                this.pagePanel.Children.Insert(0, pageButton);

                //将滚动条滚动到水平的最右边，因为插入到前面的按钮，需要在之后的滚动动画中，慢慢显示出来
                this.scrollViewer.ScrollToRightEnd();
            }
            else
            {
                this.pagePanel.Children.Add(pageButton);
            }
        }
        private int setPadding(int sum)
        {
            int b = sum.ToString().Length;
            return b;
        }
        /// <summary>
        /// 移除页码按钮
        /// </summary>
        /// <param name="deleteByFirst">页码按钮移除位置（True：移除最前面的；False：移除最后面的）</param>
        private void DeletePageButton(bool deleteByFirst)
        {
            //由于最多同时显示10个页码，计算需要删除的页码按钮个数
            int deleteCount = this.pagePanel.Children.Count - 10;
            //循环删除页码控件
            while (deleteCount > 0)
            {
                //获取页码按钮控件
                Button pageButton = null;
                if (deleteByFirst == true)
                {
                    pageButton = this.pagePanel.Children[0] as Button;
                }
                else
                {
                    pageButton = this.pagePanel.Children[this.pagePanel.Children.Count - 1] as Button;
                }
                if (pageButton != null)
                {
                    //解绑按钮事件
                    pageButton.Click -= PageButton_Click;
                    //移除按钮
                    this.pagePanel.Children.Remove(pageButton);
                }

                //递减已删除的个数
                deleteCount--;
            }
        }
        /// <summary>
        /// 页码是否已经显示中
        /// </summary>
        /// <param name="page">页码序号</param>
        /// <returns>True：已经显示；False：还未显示</returns>
        private bool PageButtonIsShowing(int page)
        {
            //初始化返回结果
            bool result = false;

            //遍历所有的分页按钮控件
            foreach (UIElement item in this.pagePanel.Children)
            {
                if (item is Button)
                {
                    //获取分页按钮控件
                    Button pageButton = item as Button;
                    //获取分页按钮对应的页码
                    int pageIndex = (pageButton.Tag is int) ? Convert.ToInt32(pageButton.Tag) : -1;
                    //判断页码与传入的页码是否相同
                    if (page == pageIndex)
                    {
                        //返回结果
                        result = true;
                        break;
                    }
                }
            }

            //返回结果
            return result;
        }
        /// <summary>
        /// 滑动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            //由于最多同时显示10个页码，计算需要删除的页码按钮个数
            int Count = this.pagePanel.Children.Count;
            if (Count > 10)
            {
                this.scrollViewer.Width = 405;
            }
            if (this._scrollCount < 3)
            {
                //计算滚动条移动后的位置
                double horizontalOffset = 0;
                if (this._scrollRight == true)
                {
                    //向右滚动
                    horizontalOffset = this.scrollViewer.HorizontalOffset + this.offset;
                    if (horizontalOffset > this.scrollViewer.ScrollableWidth)
                    {
                        horizontalOffset = this.scrollViewer.ScrollableWidth;
                    }
                }
                else
                {
                    //向左滚动
                    horizontalOffset = this.scrollViewer.HorizontalOffset - this.offset;
                    if (horizontalOffset < 0)
                    {
                        horizontalOffset = 0;
                    }
                }
                //移动滚动条
                this.scrollViewer.ScrollToHorizontalOffset(horizontalOffset);

                //累加移动次数
                this._scrollCount++;
            }
            else
            {
                //将滚动条移动到末尾或开头
                if (this._scrollRight == true)
                {
                    this.scrollViewer.ScrollToRightEnd();
                }
                else
                {
                    this.scrollViewer.ScrollToLeftEnd();
                }
                //移除多余不显示的页码按钮
                this.DeletePageButton(this._scrollRight == true);
                //停止移动按钮
                this._scrollTimer.Stop();
            }
        }
        #endregion

        #endregion
        /// <summary>
        /// 跳页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                TextBox that = sender as TextBox;
                that.Text = that.Text == "" ? "1" : that.Text;
                Regex re = new Regex("[^0-9.\\-]+");
                int thispage = int.Parse(re.IsMatch(that.Text) ? "1" : that.Text);
                if (thispage > this._pageTote)
                {
                    thispage = this._pageTote;
                }
                else if (thispage < 1)
                {
                    thispage = 1;
                }
                int startpage = 1;
                //初始化各个分页按钮的状态（最大可选的只有10个页码）
                int showPageButtonCount = this._pageTote > 10 ? 10 : this._pageTote;
                if (thispage <= 1)
                {
                    startpage = 1;
                }
                else if (thispage >= this._pageTote)
                {
                    if (this._pageTote > 9)
                    {
                        startpage = this._pageTote - 8;
                        showPageButtonCount = _pageTote;
                    }
                    else
                    {
                        startpage = 1;
                    }
                }
                else
                {
                    if (thispage - 5 > 0)
                    {
                        if (thispage + 4 <= this._pageTote)
                        {
                            startpage = thispage - 5;
                            showPageButtonCount = thispage + 4;
                        }
                        else
                        {
                            int current = 1;
                            while (thispage + current <= this._pageTote)
                            {
                                current++;
                            }
                            current = current - 1;
                            startpage = _pageTote - 9 + current;
                            showPageButtonCount = _pageTote;
                        }

                    }
                    else
                    {
                        startpage = 1;
                    }
                }
                //重置可选的分页
                this.pagePanel.Children.Clear();
                for (int i = startpage; i <= showPageButtonCount; i++)
                {
                    //添加页码按钮
                    this.AddPageButton(i, false);
                }
                //设置页码
                this.SetCurrentPage(thispage);
                that.Clear();
            }
        }
        /// <summary>
        /// 正则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.\\-]+");
            e.Handled = re.IsMatch(e.Text);
        }
        /// <summary>
        /// 页数修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (sender as ComboBox);
            switch (cb.SelectedIndex)
            {
                case 0: this.PageSize = 10; break;
                case 1: this.PageSize = 20; break;
                case 2: this.PageSize = 30; break;
                case 3: this.PageSize = 50; break;
                default:this.PageSize = 10; break;
            }
        }
    }

    /// <summary>
    /// 页码改变事件参数
    /// </summary>
    public class PagesChangedArgs : EventArgs
    {
        /// <summary>
        /// 改变前的页码
        /// </summary>
        public int OldPage { get; set; }
        /// <summary>
        /// 改变后的页码
        /// </summary>
        public int NewPage { get; set; }
    }
}
