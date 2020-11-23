namespace RxGraphicsLibrary.Tools
{
    using System;
    using System.Windows;
    using System.Windows.Media;



    public static class RxGeom
    {
        //----------------------------------------------------------------------
        // 공통함수들 모음
        //----------------------------------------------------------------------
        /// <summary>
        /// 소수점 라운딩 (UI로 표시되는 수치의 정확도를 위함)
        /// </summary>
        /// <param name="tv"></param>
        /// <returns></returns>
        public static double DoubleRound(double tv, int td = 3)
        {
            return Math.Round(tv, td, MidpointRounding.AwayFromZero);
        }


        /// <summary>
        /// Rect 라운딩 처리
        /// </summary>
        /// <param name="trct"></param>
        public static void RectBounds(ref Rect trct)
        {
            trct.Width = DoubleRound(trct.Width, 0);
            trct.Height = DoubleRound(trct.Height, 0);
            trct.X = DoubleRound(trct.X, 0);
            trct.Y = DoubleRound(trct.Y, 0);
        }


        /// <summary>
        /// 한바퀴 돌아간 라디안 보정
        /// </summary>
        /// <param name="trd"></param>
        /// <returns></returns>
        public static double CheckRadian(double trd)
        {
            if (trd < 0)
                trd = RxGeom.FullRadianHalf + trd;
            else if (trd >= RxGeom.FullRadianHalf)
                trd = trd - RxGeom.FullRadian;
            return RxGeom.DoubleRound(trd);
        }


        /// <summary>
        /// 비주얼객체 외각 사각형 구하기
        /// </summary>
        /// <param name="tpv">TempParentVisual</param>
        /// <param name="ttv">TempTargetVisual</param>
        /// <returns></returns>
        //public static Rect GetVisualBounds(Visual tpv, Visual ttv)
        //{
        //    Rect trct = VisualTreeHelper.GetContentBounds(ttv);
        //    GeneralTransform tgtf = ttv.TransformToAncestor(tpv);
        //    return tgtf.TransformBounds(trct);
        //}


        /// <summary>
        /// 비주얼객체 외각 사각형 구하기 2
        /// </summary>
        /// <param name="tpv">TempParentVisual</param>
        /// <param name="ttv">TempTargetVisual</param>
        /// <param name="trct"></param>
        /// <returns></returns>
        //public static Rect GetVisualBounds(Visual tpv, Visual ttv, Rect trct)
        //{
        //    GeneralTransform tgtf = ttv.TransformToAncestor(tpv);
        //    return tgtf.TransformBounds(trct);
        //}


        /// <summary>
        /// 비주얼객체 외각 사각형 구하기 3
        /// </summary>
        /// <param name="tpv">TempParentVisual</param>
        /// <param name="ttv">TempTargetVisual</param>
        /// <param name="trct"></param>
        /// <returns></returns>
        public static Rect GetVisualBounds(Visual ttv, Rect trct)
        {
            Visual tpv = VisualTreeHelper.GetParent(ttv) as Visual;
            GeneralTransform tgtf = ttv.TransformToAncestor(tpv);
            return tgtf.TransformBounds(trct);
        }
        //----------------------------------------------------------------------




        //----------------------------------------------------------------------
        // 기본적으로 사용되는 것들
        //----------------------------------------------------------------------
        public const double FullAngle = 360;
        public const double FullAngleHalf = FullAngle / 2;
        public const double FullAngleQuarter = FullAngle / 4;

        public const double FullRadian = Math.PI * 2;
        public const double FullRadianHalf = FullRadian / 2;
        public const double FullRadianQuarter = FullRadian / 4;

        public const double ToRadians = Math.PI / 180;
        public const double ToAngles = 180 / Math.PI;




        public static double GetAngleToRadian(double tag)
        {
            double tv = tag * ToRadians;
            return DoubleRound(tv);
        }
        public static double GetRadianToAngle(double trd)
        {
            double tv = trd * ToAngles;
            return DoubleRound(tv);
        }



        public static double GetRadian1(Matrix tmtr)
        {
            double tv = Math.Atan2(tmtr.M12, tmtr.M11);
            return DoubleRound(tv);
        }
        public static double GetRadian2(Matrix tmtr)
        {
            double tv = -Math.Atan2(tmtr.M21, tmtr.M22); ;
            return DoubleRound(tv);
        }



        public static double GetScaleX(Matrix tmtr)
        {
            double tsx = Math.Sqrt(Math.Pow(tmtr.M11, 2) + Math.Pow(tmtr.M12, 2));
            return DoubleRound(tsx);
        }
        public static double GetScaleY(Matrix tmtr)
        {
            double tsy = Math.Sqrt(Math.Pow(tmtr.M21, 2) + Math.Pow(tmtr.M22, 2));
            return DoubleRound(tsy);
        }



        public static double GetTX(Matrix tmtr)
        {
            return DoubleRound(tmtr.OffsetX);
        }
        public static double GetTY(Matrix tmtr)
        {
            return DoubleRound(tmtr.OffsetY);
        }



        public static double GetLeft(Rect trct)
        {
            return DoubleRound(trct.Left);
        }
        public static double GetTop(Rect trct)
        {
            return DoubleRound(trct.Top);
        }
        public static double GetRight(Rect trct)
        {
            return DoubleRound(trct.Right);
        }
        public static double GetBottom(Rect trct)
        {
            return DoubleRound(trct.Bottom);
        }



        public static double GetLeftCenter(Rect trct)
        {
            double tcx = trct.Left + (trct.Width / 2);
            return DoubleRound(tcx);
        }
        public static double GetTopCenter(Rect trct)
        {
            double tcy = trct.Top + (trct.Height / 2);
            return DoubleRound(tcy);
        }


        public static double GetWidth(Rect trct)
        {
            return DoubleRound(trct.Width);
        }
        public static double GetHeight(Rect trct)
        {
            return DoubleRound(trct.Height);
        }



        public static double GetHalfWidth(Rect trct)
        {
            return DoubleRound(trct.Width / 2);
        }
        public static double GetHalfHeight(Rect trct)
        {
            return DoubleRound(trct.Height / 2);
        }
        //----------------------------------------------------------------------

    }
}
