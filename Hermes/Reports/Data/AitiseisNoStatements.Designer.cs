namespace Hermes.Reports.Data
{
    partial class AitiseisNoStatements
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AitiseisNoStatements));
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.pROTOCOLGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.pROTOCOLGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.sTATION_NAMEGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.sTATION_NAMEGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.fATHER_FULLNAMECaptionTextBox = new Telerik.Reporting.TextBox();
            this.mOTHER_FULLNAMECaptionTextBox = new Telerik.Reporting.TextBox();
            this.fATHER_EMAILCaptionTextBox = new Telerik.Reporting.TextBox();
            this.mOTHER_EMAILCaptionTextBox = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.fATHER_FULLNAMEDataTextBox = new Telerik.Reporting.TextBox();
            this.mOTHER_FULLNAMEDataTextBox = new Telerik.Reporting.TextBox();
            this.fATHER_PHONEHOMEDataTextBox = new Telerik.Reporting.TextBox();
            this.mOTHER_PHONEHOMEDataTextBox = new Telerik.Reporting.TextBox();
            this.fATHER_EMAILDataTextBox = new Telerik.Reporting.TextBox();
            this.mOTHER_EMAILDataTextBox = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.sqlProskliseis = new Telerik.Reporting.SqlDataSource();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pROSKLISI_PROTOCOLDataTextBox = new Telerik.Reporting.TextBox();
            this.pROSKLISI_PROTOCOLCaptionTextBox = new Telerik.Reporting.TextBox();
            this.sTATION_NAMEDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.sqlStations = new Telerik.Reporting.SqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "Hermes.Properties.Settings.DBConnectionString";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = resources.GetString("sqlDataSource.SelectCommand");
            // 
            // pROTOCOLGroupHeaderSection
            // 
            this.pROTOCOLGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.70020031929016113D);
            this.pROTOCOLGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pROSKLISI_PROTOCOLDataTextBox,
            this.pROSKLISI_PROTOCOLCaptionTextBox});
            this.pROTOCOLGroupHeaderSection.Name = "pROTOCOLGroupHeaderSection";
            this.pROTOCOLGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // pROTOCOLGroupFooterSection
            // 
            this.pROTOCOLGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.44771653413772583D);
            this.pROTOCOLGroupFooterSection.Name = "pROTOCOLGroupFooterSection";
            this.pROTOCOLGroupFooterSection.Style.Visible = false;
            // 
            // sTATION_NAMEGroupHeaderSection
            // 
            this.sTATION_NAMEGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.999800443649292D);
            this.sTATION_NAMEGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.sTATION_NAMEDataTextBox});
            this.sTATION_NAMEGroupHeaderSection.Name = "sTATION_NAMEGroupHeaderSection";
            this.sTATION_NAMEGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // sTATION_NAMEGroupFooterSection
            // 
            this.sTATION_NAMEGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776D);
            this.sTATION_NAMEGroupFooterSection.Name = "sTATION_NAMEGroupFooterSection";
            this.sTATION_NAMEGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.54708260297775269D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.fATHER_FULLNAMECaptionTextBox,
            this.mOTHER_FULLNAMECaptionTextBox,
            this.fATHER_EMAILCaptionTextBox,
            this.mOTHER_EMAILCaptionTextBox,
            this.textBox1});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.3766670823097229D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // fATHER_FULLNAMECaptionTextBox
            // 
            this.fATHER_FULLNAMECaptionTextBox.CanGrow = true;
            this.fATHER_FULLNAMECaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.90029990673065186D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.fATHER_FULLNAMECaptionTextBox.Name = "fATHER_FULLNAMECaptionTextBox";
            this.fATHER_FULLNAMECaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.7996997833251953D), Telerik.Reporting.Drawing.Unit.Cm(0.54708260297775269D));
            this.fATHER_FULLNAMECaptionTextBox.Style.Font.Bold = true;
            this.fATHER_FULLNAMECaptionTextBox.Style.Font.Name = "Calibri";
            this.fATHER_FULLNAMECaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.fATHER_FULLNAMECaptionTextBox.StyleName = "Caption";
            this.fATHER_FULLNAMECaptionTextBox.Value = "омолатепымуло патеяа";
            // 
            // mOTHER_FULLNAMECaptionTextBox
            // 
            this.mOTHER_FULLNAMECaptionTextBox.CanGrow = true;
            this.mOTHER_FULLNAMECaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.7001996040344238D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.mOTHER_FULLNAMECaptionTextBox.Name = "mOTHER_FULLNAMECaptionTextBox";
            this.mOTHER_FULLNAMECaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.8997998237609863D), Telerik.Reporting.Drawing.Unit.Cm(0.54708182811737061D));
            this.mOTHER_FULLNAMECaptionTextBox.Style.Font.Bold = true;
            this.mOTHER_FULLNAMECaptionTextBox.Style.Font.Name = "Calibri";
            this.mOTHER_FULLNAMECaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.mOTHER_FULLNAMECaptionTextBox.StyleName = "Caption";
            this.mOTHER_FULLNAMECaptionTextBox.Value = "омолатепымуло лгтеяас";
            // 
            // fATHER_EMAILCaptionTextBox
            // 
            this.fATHER_EMAILCaptionTextBox.CanGrow = true;
            this.fATHER_EMAILCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.600199699401856D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.fATHER_EMAILCaptionTextBox.Name = "fATHER_EMAILCaptionTextBox";
            this.fATHER_EMAILCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.8997993469238281D), Telerik.Reporting.Drawing.Unit.Cm(0.54708182811737061D));
            this.fATHER_EMAILCaptionTextBox.Style.Font.Bold = true;
            this.fATHER_EMAILCaptionTextBox.Style.Font.Name = "Calibri";
            this.fATHER_EMAILCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.fATHER_EMAILCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.fATHER_EMAILCaptionTextBox.StyleName = "Caption";
            this.fATHER_EMAILCaptionTextBox.Value = "EMAIL, тгкежыма патеяа";
            // 
            // mOTHER_EMAILCaptionTextBox
            // 
            this.mOTHER_EMAILCaptionTextBox.CanGrow = true;
            this.mOTHER_EMAILCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.500200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.mOTHER_EMAILCaptionTextBox.Name = "mOTHER_EMAILCaptionTextBox";
            this.mOTHER_EMAILCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0410504341125488D), Telerik.Reporting.Drawing.Unit.Cm(0.54708182811737061D));
            this.mOTHER_EMAILCaptionTextBox.Style.Font.Bold = true;
            this.mOTHER_EMAILCaptionTextBox.Style.Font.Name = "Calibri";
            this.mOTHER_EMAILCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.mOTHER_EMAILCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.mOTHER_EMAILCaptionTextBox.StyleName = "Caption";
            this.mOTHER_EMAILCaptionTextBox.Value = "EMAIL, тгкежыма лгтеяас";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.56303280591964722D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageInfoTextBox,
            this.currentTimeTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2.9001998901367188D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1,
            this.textBox3});
            this.reportHeader.Name = "reportHeader";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2854083776474D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.mOTHER_FULLNAMEDataTextBox,
            this.fATHER_PHONEHOMEDataTextBox,
            this.mOTHER_PHONEHOMEDataTextBox,
            this.fATHER_EMAILDataTextBox,
            this.mOTHER_EMAILDataTextBox,
            this.fATHER_FULLNAMEDataTextBox,
            this.textBox2,
            this.shape1});
            this.detail.KeepTogether = true;
            this.detail.Name = "detail";
            // 
            // fATHER_FULLNAMEDataTextBox
            // 
            this.fATHER_FULLNAMEDataTextBox.CanGrow = true;
            this.fATHER_FULLNAMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.90029990673065186D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.fATHER_FULLNAMEDataTextBox.Name = "fATHER_FULLNAMEDataTextBox";
            this.fATHER_FULLNAMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.7996993064880371D), Telerik.Reporting.Drawing.Unit.Cm(0.55271697044372559D));
            this.fATHER_FULLNAMEDataTextBox.Style.Font.Name = "Calibri";
            this.fATHER_FULLNAMEDataTextBox.StyleName = "Data";
            this.fATHER_FULLNAMEDataTextBox.Value = "=Fields.FATHER_FULLNAME";
            // 
            // mOTHER_FULLNAMEDataTextBox
            // 
            this.mOTHER_FULLNAMEDataTextBox.CanGrow = true;
            this.mOTHER_FULLNAMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.7001996040344238D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.mOTHER_FULLNAMEDataTextBox.Name = "mOTHER_FULLNAMEDataTextBox";
            this.mOTHER_FULLNAMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.8997998237609863D), Telerik.Reporting.Drawing.Unit.Cm(0.55281788110733032D));
            this.mOTHER_FULLNAMEDataTextBox.Style.Font.Name = "Calibri";
            this.mOTHER_FULLNAMEDataTextBox.StyleName = "Data";
            this.mOTHER_FULLNAMEDataTextBox.Value = "=Fields.MOTHER_FULLNAME";
            // 
            // fATHER_PHONEHOMEDataTextBox
            // 
            this.fATHER_PHONEHOMEDataTextBox.CanGrow = true;
            this.fATHER_PHONEHOMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.600199699401856D), Telerik.Reporting.Drawing.Unit.Cm(0.55301773548126221D));
            this.fATHER_PHONEHOMEDataTextBox.Name = "fATHER_PHONEHOMEDataTextBox";
            this.fATHER_PHONEHOMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.8997993469238281D), Telerik.Reporting.Drawing.Unit.Cm(0.599899172782898D));
            this.fATHER_PHONEHOMEDataTextBox.Style.Font.Name = "Calibri";
            this.fATHER_PHONEHOMEDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.fATHER_PHONEHOMEDataTextBox.StyleName = "Data";
            this.fATHER_PHONEHOMEDataTextBox.Value = "=IsNull(Fields.FATHER_PHONEHOME, \"\") + \", \" + IsNull(Fields.FATHER_PHONEMOBILE, \"" +
    "\") + \", \" + IsNull(Fields.FATHER_PHONEWORK, \"\")";
            // 
            // mOTHER_PHONEHOMEDataTextBox
            // 
            this.mOTHER_PHONEHOMEDataTextBox.CanGrow = true;
            this.mOTHER_PHONEHOMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.500200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0.55301773548126221D));
            this.mOTHER_PHONEHOMEDataTextBox.Name = "mOTHER_PHONEHOMEDataTextBox";
            this.mOTHER_PHONEHOMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0410504341125488D), Telerik.Reporting.Drawing.Unit.Cm(0.59989833831787109D));
            this.mOTHER_PHONEHOMEDataTextBox.Style.Font.Name = "Calibri";
            this.mOTHER_PHONEHOMEDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.mOTHER_PHONEHOMEDataTextBox.StyleName = "Data";
            this.mOTHER_PHONEHOMEDataTextBox.Value = "=IsNull(Fields.MOTHER_PHONEHOME, \"\") + \", \" + IsNull(Fields.MOTHER_PHONEMOBILE, \"" +
    "\") + \", \" + IsNull(Fields.MOTHER_PHONEWORK, \"\")";
            // 
            // fATHER_EMAILDataTextBox
            // 
            this.fATHER_EMAILDataTextBox.CanGrow = true;
            this.fATHER_EMAILDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.600199699401856D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.fATHER_EMAILDataTextBox.Name = "fATHER_EMAILDataTextBox";
            this.fATHER_EMAILDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.8997993469238281D), Telerik.Reporting.Drawing.Unit.Cm(0.55281788110733032D));
            this.fATHER_EMAILDataTextBox.Style.Font.Name = "Calibri";
            this.fATHER_EMAILDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.fATHER_EMAILDataTextBox.StyleName = "Data";
            this.fATHER_EMAILDataTextBox.Value = "=Fields.FATHER_EMAIL";
            // 
            // mOTHER_EMAILDataTextBox
            // 
            this.mOTHER_EMAILDataTextBox.CanGrow = true;
            this.mOTHER_EMAILDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.500200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.mOTHER_EMAILDataTextBox.Name = "mOTHER_EMAILDataTextBox";
            this.mOTHER_EMAILDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0410504341125488D), Telerik.Reporting.Drawing.Unit.Cm(0.55281788110733032D));
            this.mOTHER_EMAILDataTextBox.Style.Font.Name = "Calibri";
            this.mOTHER_EMAILDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.mOTHER_EMAILDataTextBox.StyleName = "Data";
            this.mOTHER_EMAILDataTextBox.Value = "=Fields.MOTHER_EMAIL";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Hermes.Reports.A2Logo, Hermes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=n" +
    "ull";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(1.9999001026153565D));
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(2.0002000331878662D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(25.488332748413086D), Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "йатастасг аитоумтым еццяажгс стоус бмс выяис упобокг дгкысгс стоивеиым";
            // 
            // sqlProskliseis
            // 
            this.sqlProskliseis.ConnectionString = "Hermes.Properties.Settings.DBConnectionString";
            this.sqlProskliseis.Name = "sqlProskliseis";
            this.sqlProskliseis.SelectCommand = "SELECT        PROSKLISI_ID, PROTOCOL\r\nFROM            PROSKLISIS";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.28833293914795D), Telerik.Reporting.Drawing.Unit.Cm(0.56293272972106934D));
            this.pageInfoTextBox.Style.Font.Name = "Calibri";
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"сЕК.  \" +PageNumber + \"/\" + PageCount";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.0132389068603516D), Telerik.Reporting.Drawing.Unit.Cm(0.56293272972106934D));
            this.currentTimeTextBox.Style.Font.Name = "Calibri";
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pROSKLISI_PROTOCOLDataTextBox
            // 
            this.pROSKLISI_PROTOCOLDataTextBox.CanGrow = true;
            this.pROSKLISI_PROTOCOLDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.700200080871582D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.pROSKLISI_PROTOCOLDataTextBox.Name = "pROSKLISI_PROTOCOLDataTextBox";
            this.pROSKLISI_PROTOCOLDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(22.78813362121582D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.pROSKLISI_PROTOCOLDataTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.pROSKLISI_PROTOCOLDataTextBox.Style.Font.Bold = true;
            this.pROSKLISI_PROTOCOLDataTextBox.Style.Font.Name = "Calibri";
            this.pROSKLISI_PROTOCOLDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.pROSKLISI_PROTOCOLDataTextBox.StyleName = "Data";
            this.pROSKLISI_PROTOCOLDataTextBox.Value = "= Fields.PROTOCOL";
            // 
            // pROSKLISI_PROTOCOLCaptionTextBox
            // 
            this.pROSKLISI_PROTOCOLCaptionTextBox.CanGrow = true;
            this.pROSKLISI_PROTOCOLCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.pROSKLISI_PROTOCOLCaptionTextBox.Name = "pROSKLISI_PROTOCOLCaptionTextBox";
            this.pROSKLISI_PROTOCOLCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.647083044052124D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.pROSKLISI_PROTOCOLCaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.pROSKLISI_PROTOCOLCaptionTextBox.Style.Font.Bold = true;
            this.pROSKLISI_PROTOCOLCaptionTextBox.Style.Font.Name = "Calibri";
            this.pROSKLISI_PROTOCOLCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.pROSKLISI_PROTOCOLCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.pROSKLISI_PROTOCOLCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.pROSKLISI_PROTOCOLCaptionTextBox.StyleName = "Caption";
            this.pROSKLISI_PROTOCOLCaptionTextBox.Value = "пяосйкгсг :";
            // 
            // sTATION_NAMEDataTextBox
            // 
            this.sTATION_NAMEDataTextBox.CanGrow = true;
            this.sTATION_NAMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.19979965686798096D));
            this.sTATION_NAMEDataTextBox.Name = "sTATION_NAMEDataTextBox";
            this.sTATION_NAMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(25.435417175292969D), Telerik.Reporting.Drawing.Unit.Cm(0.70000040531158447D));
            this.sTATION_NAMEDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.sTATION_NAMEDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.sTATION_NAMEDataTextBox.Style.Font.Bold = true;
            this.sTATION_NAMEDataTextBox.Style.Font.Name = "Calibri";
            this.sTATION_NAMEDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.sTATION_NAMEDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.sTATION_NAMEDataTextBox.StyleName = "Data";
            this.sTATION_NAMEDataTextBox.Value = "=\"бяежомгпиайос стахлос \" + Substr(Fields.STATION_NAME, 4, len(Fields.STATION_NAM" +
    "E))";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.54708182811737061D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "A/A";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(0.00019943872757721692D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.89999985694885254D), Telerik.Reporting.Drawing.Unit.Cm(0.55271697044372559D));
            this.textBox2.Style.Font.Name = "Calibri";
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "=RowNumber() + \")\"";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(1.1531167030334473D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(25.54115104675293D), Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D));
            // 
            // sqlStations
            // 
            this.sqlStations.ConnectionString = "Hermes.Properties.Settings.DBConnectionString";
            this.sqlStations.Name = "sqlStations";
            this.sqlStations.SelectCommand = "SELECT        стахлос_йыд AS STATION_ID, епымулиа AS STATION_NAME\r\nFROM          " +
    "  SYS_STATIONS";
            // 
            // AitiseisNoStatements
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.PROSKLISI_ID", Telerik.Reporting.FilterOperator.Equal, "=Parameters.prosklisiID.Value"));
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.STATION_ID", Telerik.Reporting.FilterOperator.In, "=Parameters.stationID.Value"));
            group1.GroupFooter = this.pROTOCOLGroupFooterSection;
            group1.GroupHeader = this.pROTOCOLGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.PROTOCOL"));
            group1.Name = "pROTOCOLGroup";
            group2.GroupFooter = this.sTATION_NAMEGroupFooterSection;
            group2.GroupHeader = this.sTATION_NAMEGroupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.STATION_NAME"));
            group2.Name = "sTATION_NAMEGroup";
            group3.GroupFooter = this.labelsGroupFooterSection;
            group3.GroupHeader = this.labelsGroupHeaderSection;
            group3.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pROTOCOLGroupHeaderSection,
            this.pROTOCOLGroupFooterSection,
            this.sTATION_NAMEGroupHeaderSection,
            this.sTATION_NAMEGroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.reportHeader,
            this.detail});
            this.Name = "AitiseisNoStatements";
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
            reportParameter2.AllowNull = true;
            reportParameter2.AutoRefresh = true;
            reportParameter2.AvailableValues.DataSource = this.sqlStations;
            reportParameter2.AvailableValues.DisplayMember = "= Fields.STATION_NAME";
            reportParameter2.AvailableValues.ValueMember = "= Fields.STATION_ID";
            reportParameter2.MultiValue = true;
            reportParameter2.Name = "stationID";
            reportParameter2.Text = "бмс";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
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
        private Telerik.Reporting.GroupFooterSection pROTOCOLGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection sTATION_NAMEGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection sTATION_NAMEGroupFooterSection;
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
        private Telerik.Reporting.TextBox fATHER_PHONEHOMEDataTextBox;
        private Telerik.Reporting.TextBox mOTHER_PHONEHOMEDataTextBox;
        private Telerik.Reporting.TextBox fATHER_EMAILDataTextBox;
        private Telerik.Reporting.TextBox mOTHER_EMAILDataTextBox;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.SqlDataSource sqlProskliseis;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pROSKLISI_PROTOCOLDataTextBox;
        private Telerik.Reporting.TextBox pROSKLISI_PROTOCOLCaptionTextBox;
        private Telerik.Reporting.TextBox sTATION_NAMEDataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.SqlDataSource sqlStations;

    }
}