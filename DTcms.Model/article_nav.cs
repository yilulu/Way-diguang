using System;
using System.Collections.Generic;
using System.Text;

namespace DTcms.Model
{
    /// <summary>
    /// 导航模型
    /// </summary>
    [Serializable]
    public partial class article_nav
    {
        public article_nav() { }
        #region
        private int _n_id;

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        private string _n_title;

        public string n_title
        {
            get { return _n_title; }
            set { _n_title = value; }
        }
        private string _n_url;

        public string n_url
        {
            get { return _n_url; }
            set { _n_url = value; }
        }
        private int _n_state;

        public int n_state
        {
            get { return _n_state; }
            set { _n_state = value; }
        }
        private int _n_sequence;

        public int n_sequence
        {
            get { return _n_sequence; }
            set { _n_sequence = value; }
        }
        private string _n_desc;

        public string n_desc
        {
            get { return _n_desc; }
            set { _n_desc = value; }
        }
        #endregion

    }
}
