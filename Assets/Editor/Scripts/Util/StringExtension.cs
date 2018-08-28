using System;

namespace NodeEditor.Util
{
	public static class StringExtension
	{
		private static int Minimum(int a, int b, int c)
		{
			int mi;

			mi = a;
			if (b < mi)
			{
				mi = b;
			}
			if (c < mi)
			{
				mi = c;
			}
			return mi;

		}

		public static int LevenshteinDistance(this string s, string t)
		{
			int[,] d; // matrix
			int n; // length of s
			int m; // length of t
			int i; // iterates through s
			int j; // iterates through t
			char s_i; // ith character of s
			char t_j; // jth character of t
			int cost; // cost

			// Step 1

			n = s.Length;
			m = t.Length;
			if (n == 0)
			{
				return m;
			}
			if (m == 0)
			{
				return n;
			}
			d = new int[n + 1, m + 1];

			// Step 2

			for (i = 0; i <= n; i++)
			{
				d[i, 0] = i;
			}

			for (j = 0; j <= m; j++)
			{
				d[0, j] = j;
			}

			// Step 3

			for (i = 1; i <= n; i++)
			{

				s_i = s[i - 1];

				// Step 4

				for (j = 1; j <= m; j++)
				{

					t_j = t[j - 1];

					// Step 5

					if (s_i == t_j)
					{
						cost = 0;
					}
					else
					{
						cost = 1;
					}

					// Step 6

					d[i, j] = Minimum(d[i - 1, j] + 1, d[i, j - 1] + 1, d[i - 1, j - 1] + cost);

				}

			}

			// Step 7

			return d[n, m];

		}

	}
}