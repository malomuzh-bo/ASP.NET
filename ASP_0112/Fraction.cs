using System;

namespace ASP_0112
{
	public class Fraction
	{
		public int Num_whole { get; set; }
		public int Numerator { get; set; }
		public int Denominator { get; set; }

		public Fraction()
		{
			Numerator = 1;
			Denominator = 1;
		}

		public Fraction(decimal number)
		{
			var numStr = number.ToString().Split(",");
			Num_whole = int.Parse(numStr[0]);
			if (numStr.Length > 1)
			{
				Numerator = int.Parse(numStr[1]);
				Denominator = (int)Math.Pow(10, numStr[1].Length);
				int nod = getNode(Numerator, Denominator);
				Numerator /= nod;
				Denominator /= nod;
			}
			else
			{
				Numerator = 0; Denominator = 0;
			}
		}

		public Fraction(int num, int denum, int whole = 0)
		{
			Num_whole = whole;
			int nod = getNode(num, denum);
			Numerator = num / nod;
			Denominator = denum / nod;
			if (Numerator == 0 && Denominator == 1)
			{
				Numerator = Denominator = 0;
			}
		}

		public override string ToString()
		{
			return $"{(Num_whole != 0 ? $"{Num_whole} whole " : "")}" +
				   $"{(Numerator != 0 ? $"{Numerator}/" : "")}" +
				   $"{(Denominator != 0 ? $"{Denominator}" : "")}";
		}

		public static int getNode(int num, int num2)
		{
			num = Math.Abs(num);
			while ((num != 0) && (num2 != 0))
			{
				if (num > num2)
					num %= num2;
				else
					num2 %= num;
			}
			return Math.Max(num, num2);
		}

		public Fraction plus(Fraction fraction)
		{
			Fraction res = new Fraction();
			var fr1 = nonWriteFract(this);
			var fr2 = nonWriteFract(fraction);
			fr1.Numerator *= fr2.Denominator;
			fr2.Numerator *= fr1.Denominator;
			res.Numerator = fr1.Numerator + fr2.Numerator;
			res.Denominator = fr1.Denominator * fr2.Denominator;
			int nod = getNode(res.Numerator, res.Denominator);
			res.Numerator /= nod;
			res.Denominator /= nod;
			return res.writeFract(res);
		}

		public Fraction minus(Fraction fraction)
		{
			Fraction res = new Fraction();
			var fr1 = nonWriteFract(this);
			var fr2 = nonWriteFract(fraction);
			fr1.Numerator *= fr2.Denominator;
			fr2.Numerator *= fr1.Denominator;
			res.Numerator = fr1.Numerator - fr2.Numerator;
			res.Denominator = fr1.Denominator * fr2.Denominator;
			int nod = getNode(res.Numerator, res.Denominator);
			res.Numerator /= nod;
			res.Denominator /= nod;
			return res.writeFract(res);
		}

		public Fraction mult(Fraction fraction)
		{
			Fraction res = new Fraction();
			var fr1 = nonWriteFract(this);
			var fr2 = nonWriteFract(fraction);
			res.Numerator = fr1.Numerator * fr2.Numerator;
			res.Denominator = fr1.Denominator * fr2.Denominator;
			int nod = getNode(res.Numerator, res.Denominator);
			res.Numerator /= nod;
			res.Denominator /= nod;
			return res.writeFract(res);
		}

		public Fraction div(Fraction fraction)
		{
			Fraction res = new Fraction();
			var fr1 = nonWriteFract(this);
			var tmp = nonWriteFract(fraction);
			var fr2 = new Fraction(tmp.Denominator, tmp.Numerator);
			res.Numerator = fr1.Numerator * fr2.Denominator;
			res.Denominator = fr2.Numerator * fr1.Denominator;
			int node = getNode(res.Numerator, res.Denominator);
			res.Numerator /= node;
			res.Denominator /= node;
			return res.writeFract(res);
		}


		public Fraction nonWriteFract(Fraction fr)
		{
			return new Fraction((fr.Denominator * fr.Num_whole + fr.Numerator), fr.Denominator);
		}

		public Fraction writeFract(Fraction fr)
		{
			int whole = (int)(fr.Numerator / fr.Denominator);
			if (whole == 0)
				return fr;
			else return new Fraction(fr.Numerator - whole * fr.Denominator, fr.Denominator, whole + fr.Num_whole);
		}
		public decimal decimalFract()
		{
			return decimal.Parse($"{this.Num_whole + (this.Numerator != 0 ? (this.Numerator * 1.0 / this.Denominator) : 0)}");
		}
	}
}
