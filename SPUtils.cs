namespace TestGenericShooter
{
    public static class SPUtils
    {
        public const int Divisor = 100;

        public static int ToUnits(this int mValue) { return mValue*Divisor; }

        public static int ToPixels(this int mValue) { return mValue/Divisor; }

        public static string CalculateWall(this string[] mMap, int mX, int mY)
        {
            #region Fill / Single / Single Cross
            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (!IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1) &&
                !IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY))
            {
                return ("single");
            }

            if (IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1) &&
                IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY + 1))
            {
                return ("fill");
            }
            #endregion

            #region End To Fill
            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }
            #endregion

            #region Internal Fill Corner
            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }
            #endregion

            #region Internal Double
            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }
            #endregion

            #region X
            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1))
            {
                return ("fill");
            }
            #endregion

            #region Single Vertical / Horizontal
            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("single_horiz");

            if (!IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("single_vert");
            #endregion

            #region End
            if (IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("end_w");

            if (!IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("end_e");

            if (!IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("end_n");

            if (!IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("end_s");
            #endregion

            #region Single Corner Diagonal
            if (IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("fill_corner_se");

            if (!IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("fill_corner_sw");

            if (IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_ne");

            if (!IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_nw");
            #endregion

            #region Fill Corner Diagonal
            if (IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("fill_corner_se");

            if (!IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("fill_corner_sw");

            if (IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_ne");

            if (!IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                !IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_nw");
            #endregion

            #region Fill Corner Orthogonal
            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                !IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_n");

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("fill_corner_s");

            if (!IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_w");

            if (IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_e");
            #endregion

            #region T
            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("fill_corner_s");

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                !IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_n");

            if (!IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_w");

            if (IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_e");
            #endregion

            #region Y
            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                !IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_n");

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX, mY + 1) &&
                IsWall(mMap, mX, mY - 1)) return ("fill_corner_s");

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                !IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_n");

            if (!IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX + 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_w");

            if (IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX, mY - 1) &&
                !IsWall(mMap, mX, mY + 1)) return ("fill_corner_s");

            if (IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                IsWall(mMap, mX - 1, mY - 1) &&
                !IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_e");

            if (!IsWall(mMap, mX - 1, mY) &&
                IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX + 1, mY - 1) &&
                IsWall(mMap, mX + 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_w");

            if (IsWall(mMap, mX - 1, mY) &&
                !IsWall(mMap, mX + 1, mY) &&
                !IsWall(mMap, mX - 1, mY - 1) &&
                IsWall(mMap, mX - 1, mY + 1) &&
                IsWall(mMap, mX, mY - 1) &&
                IsWall(mMap, mX, mY + 1)) return ("fill_corner_e");
            #endregion

            return "fill";
        }

        public static bool IsValue(this string[] mMap, int mX, int mY, int mValue) { return mMap[mY].Substring(mX, 1) == mValue.ToString(); }

        private static bool IsWall(this string[] mMap, int mX, int mY)
        {
            if (mX <= 0 || mX >= mMap[0].Length - 1 || mY <= 0 || mY >= mMap.GetLength(0) - 1) return true;
            return IsValue(mMap, mX, mY, 1);
        }
    }
}