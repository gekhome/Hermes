namespace Hermes.Reports.Misc
{
    partial class ParentsEmails
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.pROTOCOLGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.pROTOCOLGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.pROTOCOLCaptionTextBox = new Telerik.Reporting.TextBox();
            this.pROTOCOLDataTextBox = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.fATHER_FULLNAMECaptionTextBox = new Telerik.Reporting.TextBox();
            this.mOTHER_FULLNAMECaptionTextBox = new Telerik.Reporting.TextBox();
            this.fATHER_EMAILCaptionTextBox = new Telerik.Reporting.TextBox();
            this.mOTHER_EMAILCaptionTextBox = new Telerik.Reporting.TextBox();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.fATHER_FULLNAMEDataTextBox = new Telerik.Reporting.TextBox();
            this.mOTHER_FULLNAMEDataTextBox = new Telerik.Reporting.TextBox();
            this.fATHER_EMAILDataTextBox = new Telerik.Reporting.TextBox();
            this.mOTHER_EMAILDataTextBox = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.sqlProskliseis = new Telerik.Reporting.SqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pROTOCOLGroupFooterSection
            // 
            this.pROTOCOLGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.pROTOCOLGroupFooterSection.Name = "pROTOCOLGroupFooterSection";
            this.pROTOCOLGroupFooterSection.Style.Visible = false;
            // 
            // pROTOCOLGroupHeaderSection
            // 
            this.pROTOCOLGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.89999991655349731D);
            this.pROTOCOLGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pROTOCOLCaptionTextBox,
            this.pROTOCOLDataTextBox});
            this.pROTOCOLGroupHeaderSection.Name = "pROTOCOLGroupHeaderSection";
            // 
            // pROTOCOLCaptionTextBox
            // 
            this.pROTOCOLCaptionTextBox.CanGrow = true;
            this.pROTOCOLCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pROTOCOLCaptionTextBox.Name = "pROTOCOLCaptionTextBox";
            this.pROTOCOLCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.3468832969665527D), Telerik.Reporting.Drawing.Unit.Cm(0.79999995231628418D));
            this.pROTOCOLCaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.pROTOCOLCaptionTextBox.Style.Font.Bold = true;
            this.pROTOCOLCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.pROTOCOLCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.pROTOCOLCaptionTextBox.StyleName = "Caption";
            this.pROTOCOLCaptionTextBox.Value = "PROTOCOL:";
            // 
            // pROTOCOLDataTextBox
            // 
            this.pROTOCOLDataTextBox.CanGrow = true;
            this.pROTOCOLDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.4000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pROTOCOLDataTextBox.Name = "pROTOCOLDataTextBox";
            this.pROTOCOLDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(21.088333129882812D), Telerik.Reporting.Drawing.Unit.Cm(0.79999995231628418D));
            this.pROTOCOLDataTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.pROTOCOLDataTextBox.Style.Font.Bold = true;
            this.pROTOCOLDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.pROTOCOLDataTextBox.StyleName = "Data";
            this.pROTOCOLDataTextBox.Value = "=Fields.PROTOCOL";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.70010024309158325D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.fATHER_FULLNAMECaptionTextBox,
            this.mOTHER_FULLNAMECaptionTextBox,
            this.fATHER_EMAILCaptionTextBox,
            this.mOTHER_EMAILCaptionTextBox,
            this.textBox2});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // fATHER_FULLNAMECaptionTextBox
            // 
            this.fATHER_FULLNAMECaptionTextBox.CanGrow = true;
            this.fATHER_FULLNAMECaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.2999997138977051D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.fATHER_FULLNAMECaptionTextBox.Name = "fATHER_FULLNAMECaptionTextBox";
            this.fATHER_FULLNAMECaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.fATHER_FULLNAMECaptionTextBox.Style.Font.Bold = true;
            this.fATHER_FULLNAMECaptionTextBox.Style.Font.Name = "Calibri";
            this.fATHER_FULLNAMECaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.fATHER_FULLNAMECaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.fATHER_FULLNAMECaptionTextBox.StyleName = "Caption";
            this.fATHER_FULLNAMECaptionTextBox.Value = "омолатепымуло патеяа";
            // 
            // mOTHER_FULLNAMECaptionTextBox
            // 
            this.mOTHER_FULLNAMECaptionTextBox.CanGrow = true;
            this.mOTHER_FULLNAMECaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.1002001762390137D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.mOTHER_FULLNAMECaptionTextBox.Name = "mOTHER_FULLNAMECaptionTextBox";
            this.mOTHER_FULLNAMECaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.8998003005981445D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.mOTHER_FULLNAMECaptionTextBox.Style.Font.Bold = true;
            this.mOTHER_FULLNAMECaptionTextBox.Style.Font.Name = "Calibri";
            this.mOTHER_FULLNAMECaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.mOTHER_FULLNAMECaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.mOTHER_FULLNAMECaptionTextBox.StyleName = "Caption";
            this.mOTHER_FULLNAMECaptionTextBox.Value = "омолатепымуло лгтеяас";
            // 
            // fATHER_EMAILCaptionTextBox
            // 
            this.fATHER_EMAILCaptionTextBox.CanGrow = true;
            this.fATHER_EMAILCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.000201225280762D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.fATHER_EMAILCaptionTextBox.Name = "fATHER_EMAILCaptionTextBox";
            this.fATHER_EMAILCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.3997998237609863D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.fATHER_EMAILCaptionTextBox.Style.Font.Bold = true;
            this.fATHER_EMAILCaptionTextBox.Style.Font.Name = "Calibri";
            this.fATHER_EMAILCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.fATHER_EMAILCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.fATHER_EMAILCaptionTextBox.StyleName = "Caption";
            this.fATHER_EMAILCaptionTextBox.Value = "E-MAIL патеяа";
            // 
            // mOTHER_EMAILCaptionTextBox
            // 
            this.mOTHER_EMAILCaptionTextBox.CanGrow = true;
            this.mOTHER_EMAILCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.400201797485352D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.mOTHER_EMAILCaptionTextBox.Name = "mOTHER_EMAILCaptionTextBox";
            this.mOTHER_EMAILCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0881328582763672D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.mOTHER_EMAILCaptionTextBox.Style.Font.Bold = true;
            this.mOTHER_EMAILCaptionTextBox.Style.Font.Name = "Calibri";
            this.mOTHER_EMAILCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.mOTHER_EMAILCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.mOTHER_EMAILCaptionTextBox.StyleName = "Caption";
            this.mOTHER_EMAILCaptionTextBox.Value = "E-MAIL лгтеяас";
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "Hermes.Properties.Settings.DBConnectionString";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = "SELECT        FATHER_FULLNAME, MOTHER_FULLNAME, FATHER_EMAIL, MOTHER_EMAIL, PROSK" +
    "LISI_ID, PROTOCOL\r\nFROM            adm_PARENTS_EMAILS\r\nORDER BY FATHER_FULLNAME," +
    " MOTHER_FULLNAME";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60083413124084473D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageInfoTextBox,
            this.currentTimeTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.400200843811035D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(12.088132858276367D), Telerik.Reporting.Drawing.Unit.Cm(0.60073369741439819D));
            this.pageInfoTextBox.Style.Font.Name = "Calibri";
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=PageNumber + \"/\"+ PageCount";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.347084045410156D), Telerik.Reporting.Drawing.Unit.Cm(0.60083377361297607D));
            this.currentTimeTextBox.Style.Font.Name = "Calibri";
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2.5D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1,
            this.textBox1});
            this.reportHeader.Name = "reportHeader";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50010019540786743D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.fATHER_FULLNAMEDataTextBox,
            this.mOTHER_FULLNAMEDataTextBox,
            this.fATHER_EMAILDataTextBox,
            this.mOTHER_EMAILDataTextBox,
            this.textBox3});
            this.detail.Name = "detail";
            // 
            // fATHER_FULLNAMEDataTextBox
            // 
            this.fATHER_FULLNAMEDataTextBox.CanGrow = true;
            this.fATHER_FULLNAMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.2999997138977051D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.fATHER_FULLNAMEDataTextBox.Name = "fATHER_FULLNAMEDataTextBox";
            this.fATHER_FULLNAMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.49990001320838928D));
            this.fATHER_FULLNAMEDataTextBox.Style.Font.Name = "Calibri";
            this.fATHER_FULLNAMEDataTextBox.StyleName = "Data";
            this.fATHER_FULLNAMEDataTextBox.Value = "=Fields.FATHER_FULLNAME";
            // 
            // mOTHER_FULLNAMEDataTextBox
            // 
            this.mOTHER_FULLNAMEDataTextBox.CanGrow = true;
            this.mOTHER_FULLNAMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.1002001762390137D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.mOTHER_FULLNAMEDataTextBox.Name = "mOTHER_FULLNAMEDataTextBox";
            this.mOTHER_FULLNAMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.8998003005981445D), Telerik.Reporting.Drawing.Unit.Cm(0.49990001320838928D));
            this.mOTHER_FULLNAMEDataTextBox.Style.Font.Name = "Calibri";
            this.mOTHER_FULLNAMEDataTextBox.StyleName = "Data";
            this.mOTHER_FULLNAMEDataTextBox.Value = "=Fields.MOTHER_FULLNAME";
            // 
            // fATHER_EMAILDataTextBox
            // 
            this.fATHER_EMAILDataTextBox.CanGrow = true;
            this.fATHER_EMAILDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.000201225280762D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.fATHER_EMAILDataTextBox.Name = "fATHER_EMAILDataTextBox";
            this.fATHER_EMAILDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.3997998237609863D), Telerik.Reporting.Drawing.Unit.Cm(0.49990001320838928D));
            this.fATHER_EMAILDataTextBox.Style.Font.Name = "Calibri";
            this.fATHER_EMAILDataTextBox.StyleName = "Data";
            this.fATHER_EMAILDataTextBox.Value = "=Fields.FATHER_EMAIL";
            // 
            // mOTHER_EMAILDataTextBox
            // 
            this.mOTHER_EMAILDataTextBox.CanGrow = true;
            this.mOTHER_EMAILDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.400201797485352D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.mOTHER_EMAILDataTextBox.Name = "mOTHER_EMAILDataTextBox";
            this.mOTHER_EMAILDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0881328582763672D), Telerik.Reporting.Drawing.Unit.Cm(0.49990001320838928D));
            this.mOTHER_EMAILDataTextBox.Style.Font.Name = "Calibri";
            this.mOTHER_EMAILDataTextBox.StyleName = "Data";
            this.mOTHER_EMAILDataTextBox.Value = "=Fields.MOTHER_EMAIL";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Hermes.Reports.A2Logo, Hermes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=n" +
    "ull";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.6470832824707031D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.7002010345458984D), Telerik.Reporting.Drawing.Unit.Cm(0.00010002215276472271D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.788132667541504D), Telerik.Reporting.Drawing.Unit.Cm(2.1998999118804932D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "йатастасг диеухумсеым E-MAIL аитоумтым цомеым ама пяосйкгсг";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2468831539154053D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Calibri";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "A/A";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2468831539154053D), Telerik.Reporting.Drawing.Unit.Cm(0.49990001320838928D));
            this.textBox3.Style.Font.Name = "Calibri";
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.StyleName = "Data";
            this.textBox3.Value = "=RowNumber() + \".\"";
            // 
            // sqlProskliseis
            // 
            this.sqlProskliseis.ConnectionString = "Hermes.Properties.Settings.DBConnectionString";
            this.sqlProskliseis.Name = "sqlProskliseis";
            this.sqlProskliseis.SelectCommand = "SELECT        PROSKLISI_ID, PROTOCOL\r\nFROM            PROSKLISIS";
            // 
            // ParentsEmails
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.PROSKLISI_ID", Telerik.Reporting.FilterOperator.Equal, "=Parameters.prosklisiID.Value"));
            group1.GroupFooter = this.pROTOCOLGroupFooterSection;
            group1.GroupHeader = this.pROTOCOLGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.PROTOCOL"));
            group1.Name = "pROTOCOLGroup";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pROTOCOLGroupHeaderSection,
            this.pROTOCOLGroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.reportHeader,
            this.detail});
            this.Name = "ParentsEmails";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlProskliseis;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.PROTOCOL";
            reportParameter1.AvailableValues.ValueMember = "= Fields.PROSKLISI_ID";
            reportParameter1.Name = "prosklisiID";
            reportParameter1.Text = "пЯЭСЙКГСГ";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = true;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(25.594165802001953D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource;
        private Telerik.Reporting.GroupHeaderSection pROTOCOLGroupHeaderSection;
        private Telerik.Reporting.TextBox pROTOCOLCaptionTextBox;
        private Telerik.Reporting.TextBox pROTOCOLDataTextBox;
        private Telerik.Reporting.GroupFooterSection pROTOCOLGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox fATHER_FULLNAMECaptionTextBox;
        private Telerik.Reporting.TextBox mOTHER_FULLNAMECaptionTextBox;
        private Telerik.Reporting.TextBox fATHER_EMAILCaptionTextBox;
        private Telerik.Reporting.TextBox mOTHER_EMAILCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox fATHER_FULLNAMEDataTextBox;
        private Telerik.Reporting.TextBox mOTHER_FULLNAMEDataTextBox;
        private Telerik.Reporting.TextBox fATHER_EMAILDataTextBox;
        private Telerik.Reporting.TextBox mOTHER_EMAILDataTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.SqlDataSource sqlProskliseis;

    }
}