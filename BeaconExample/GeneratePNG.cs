
using System.Drawing;

namespace BeaconExample
{
      class generatePNG
    {
        public static void createIMG(double distanceA, double distanceB, double distanceC)
        {
            using (Bitmap b = new Bitmap(3752, 4304))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(Color.Transparent);
                    Pen pen = new Pen(Brushes.Red);
                    // The set floats are the pixel mappings on the png.
                    float centerXA = 2435F;
                    float centerYA = 3150F;
                    float centerXB = 1900F;
                    float centerYB = 2970F;
                    float centerXC = 1276F;
                    float centerYC = 3170F;
                    // 1px = 1.62m
                    float radiusA = (float)((int)(distanceA * 100) / 1.296);
                    float radiusB = (float)((int)(distanceB * 100) / 1.296);
                    float radiusC = (float)((int)(distanceC * 100) / 1.296);
                    Image image = Image.FromFile("C: \\Users\\dklomp1\\Pictures\\location test\\West 3 plattegrond.png");
                    g.DrawImage(image,0,0 );
                    g.DrawEllipse(pen, centerXA - radiusA, centerYA - radiusA,
                          radiusA + radiusA, radiusA + radiusA);
                    g.DrawEllipse(pen, centerXB - radiusB, centerYB - radiusB,
                          radiusB + radiusB, radiusB + radiusB);
                    g.DrawEllipse(pen, centerXC - radiusC, centerYC - radiusC,
                          radiusC + radiusC, radiusC + radiusC);

                }

                b.Save(@"C:\Users\dklomp1\Pictures\location test\test.png");
            }
        }
        public static void createIMGAlt(double distanceA, double distanceB, double distanceC)
        {
            using (Bitmap b = new Bitmap(3752, 4304))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(Color.Transparent);
                    Pen pen = new Pen(Brushes.Red);
                    // The set floats are the pixel mappings on the png.
                    float centerXA = 2435F;
                    float centerYA = 3150F;
                    float centerXB = 1900F;
                    float centerYB = 2970F;
                    float centerXC = 1276F;
                    float centerYC = 3170F;
                    // 1px = 1.62m
                    float radiusA = (float)((int)(distanceA * 100) / 1.62);
                    float radiusB = (float)((int)(distanceB * 100) / 1.62);
                    float radiusC = (float)((int)(distanceC * 100) / 1.62);
                    Image image = Image.FromFile("C: \\Users\\dklomp1\\Pictures\\location test\\West 3 plattegrond.png");
                    g.DrawImage(image, 0, 0);
                    g.DrawEllipse(pen, centerXA - radiusA, centerYA - radiusA,
                          radiusA + radiusA, radiusA + radiusA);
                    g.DrawEllipse(pen, centerXB - radiusB, centerYB - radiusB,
                          radiusB + radiusB, radiusB + radiusB);
                    g.DrawEllipse(pen, centerXC - radiusC, centerYC - radiusC,
                          radiusC + radiusC, radiusC + radiusC);

                }

                b.Save(@"C:\Users\dklomp1\Pictures\location test\testAlt.png");
            }
        }
    }
}
