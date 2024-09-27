using DevExpress.XtraCharts;
using System.Diagnostics;
using System.Windows.Forms;

namespace FormTest;

public partial class Form1 : DevExpress.XtraEditors.XtraForm {
  public Form1() {
    InitializeComponent();
  }
  public void Form() {
    var chartControl = new ChartControl();
    Series innerSeries = new Series("Inner Series", ViewType.Doughnut);
    Series outerSeries = new Series("Outer Series", ViewType.Doughnut);
    NestedDoughnutSeriesView innerView = new NestedDoughnutSeriesView();
    NestedDoughnutSeriesView outerView = new NestedDoughnutSeriesView();
    innerSeries.View = innerView;
    outerSeries.View = outerView;

    innerSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
    outerSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

    DoughnutSeriesLabel innerLabel = (DoughnutSeriesLabel)innerSeries.Label;
    innerLabel.TextPattern = "{A}: {V} ({VP:P0})";

    DoughnutSeriesLabel outerLabel = (DoughnutSeriesLabel)outerSeries.Label;
    outerLabel.TextPattern = "{A}: {V} ({VP:P0})";

    // Adding test data to innerSeries
    innerSeries.Points.Add(new SeriesPoint("Category A", 40));
    innerSeries.Points.Add(new SeriesPoint("Category B", 30));
    innerSeries.Points.Add(new SeriesPoint("Category C", 20));
    innerSeries.Points.Add(new SeriesPoint("Category D", 10));

    // Adding test data to outerSeries
    outerSeries.Points.Add(new SeriesPoint("Subcategory A1", 20));
    outerSeries.Points.Add(new SeriesPoint("Subcategory A2", 20));
    outerSeries.Points.Add(new SeriesPoint("Subcategory B1", 15));
    outerSeries.Points.Add(new SeriesPoint("Subcategory B2", 15));
    outerSeries.Points.Add(new SeriesPoint("Subcategory C1", 10));
    outerSeries.Points.Add(new SeriesPoint("Subcategory C2", 10));
    outerSeries.Points.Add(new SeriesPoint("Subcategory D1", 5));
    outerSeries.Points.Add(new SeriesPoint("Subcategory D2", 5));

    chartControl.Series.AddRange(new Series[] { outerSeries, innerSeries });
    chartControl.Dock = DockStyle.Fill;
    Size = new System.Drawing.Size(800, 600);
    Controls.Add(chartControl);

    chartControl.MouseClick += (s, e) => {
      var hitInfo = chartControl.CalcHitInfo(e.Location);
      Debug.WriteLine(hitInfo);
      Debug.WriteLine(hitInfo.InSeries.ToString());
    };

    chartControl.CustomDrawSeriesPoint += (s, e) => {
      if (e.SeriesPoint == innerSeries.Points[0]) {
        e.LabelText = $"{e.SeriesPoint.Argument}: {e.SeriesPoint.Values[0]} ({e.SeriesPoint.Values[0] / 100:P0})";
      }
      else if (e.Series == outerSeries) {
        e.LabelText = $"{e.SeriesPoint.Argument}: {e.SeriesPoint.Values[0]} ({e.SeriesPoint.Values[0] / 100:P0})";
      }
      else {
        e.LabelText = string.Empty;
      }
    };
  }

}
