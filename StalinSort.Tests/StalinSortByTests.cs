using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StalinSort.Tests {

	[TestClass]
	public class StalinSortByTests {

		private static readonly IReadOnlyList<int> INT_LIST = new List<int> { -5, 1, -1, 0, 2, 3, 4, 80, 80, 70, 60, 50, -100, 1000000, 100, 200, 300 };
		private static readonly IReadOnlyList<string> STRING_LIST = new List<string> { "the", "quick", "", null, "", "brown", "", "fox", "jumps", "over", "the lazy", "DOG" };

		[TestMethod]
		public void StalinSortBy_Int32() {
			List<int> sortedList = INT_LIST.StalinSortBy(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(8, sortedList.Count);
			Assert.AreEqual(-5, sortedList[0]);
			Assert.AreEqual(1, sortedList[1]);
			Assert.AreEqual(2, sortedList[2]);
			Assert.AreEqual(3, sortedList[3]);
			Assert.AreEqual(4, sortedList[4]);
			Assert.AreEqual(80, sortedList[5]);
			Assert.AreEqual(80, sortedList[6]);
			Assert.AreEqual(1000000, sortedList[7]);

			// repeated stalin sort
			sortedList = INT_LIST
				.StalinSortBy(element => element)
				.StalinSortBy(element => element)
				.StalinSortBy(element => element)
				.StalinSortBy(element => element)
				.ToList()
				.StalinSortBy(element => element)
				.StalinSortBy(element => element)
				.ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(8, sortedList.Count);
			Assert.AreEqual(-5, sortedList[0]);
			Assert.AreEqual(1, sortedList[1]);
			Assert.AreEqual(2, sortedList[2]);
			Assert.AreEqual(3, sortedList[3]);
			Assert.AreEqual(4, sortedList[4]);
			Assert.AreEqual(80, sortedList[5]);
			Assert.AreEqual(80, sortedList[6]);
			Assert.AreEqual(1000000, sortedList[7]);

			// -5, -1, -100
			sortedList = INT_LIST.Where(element => element < 0).StalinSortBy(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(2, sortedList.Count);
			Assert.AreEqual(-5, sortedList[0]);
			Assert.AreEqual(-1, sortedList[1]);

			// empty sequence
			sortedList = INT_LIST.Where(element => false).StalinSortBy(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(0, sortedList.Count);

			// ordered sequence
			sortedList = INT_LIST.OrderBy(element => element).StalinSortBy(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(INT_LIST.Count, sortedList.Count);
		}

		[TestMethod]
		public void StalinSortBy_String() {
			List<string> sortedList = STRING_LIST.StalinSortBy(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(2, sortedList.Count);
			Assert.AreEqual("the", sortedList[0]);
			Assert.AreEqual("the lazy", sortedList[1]);

			// "" then null
			sortedList = STRING_LIST.Skip(2).StalinSortBy(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(7, sortedList.Count);
			Assert.AreEqual("", sortedList[0]);
			Assert.AreEqual("", sortedList[1]);
			Assert.AreEqual("brown", sortedList[2]);
			Assert.AreEqual("fox", sortedList[3]);
			Assert.AreEqual("jumps", sortedList[4]);
			Assert.AreEqual("over", sortedList[5]);
			Assert.AreEqual("the lazy", sortedList[6]);

			// null then ""
			sortedList = STRING_LIST.Skip(3).StalinSortBy(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(7, sortedList.Count);
			Assert.AreEqual(null, sortedList[0]);
			Assert.AreEqual("", sortedList[1]);
			Assert.AreEqual("brown", sortedList[2]);
			Assert.AreEqual("fox", sortedList[3]);
			Assert.AreEqual("jumps", sortedList[4]);
			Assert.AreEqual("over", sortedList[5]);
			Assert.AreEqual("the lazy", sortedList[6]);
		}

		[TestMethod]
		public void StalinSortBy_String_ByLength() {
			List<string> sortedList = STRING_LIST.StalinSortBy(element => element?.Length).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(5, sortedList.Count);
			Assert.AreEqual("the", sortedList[0]);
			Assert.AreEqual("quick", sortedList[1]);
			Assert.AreEqual("brown", sortedList[2]);
			Assert.AreEqual("jumps", sortedList[3]);
			Assert.AreEqual("the lazy", sortedList[4]);

			// "" then null
			sortedList = STRING_LIST.Skip(2).StalinSortBy(element => element?.Length).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(5, sortedList.Count);
			Assert.AreEqual("", sortedList[0]);
			Assert.AreEqual("", sortedList[1]);
			Assert.AreEqual("brown", sortedList[2]);
			Assert.AreEqual("jumps", sortedList[3]);
			Assert.AreEqual("the lazy", sortedList[4]);

			// null then ""
			sortedList = STRING_LIST.Skip(3).StalinSortBy(element => element?.Length).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(5, sortedList.Count);
			Assert.AreEqual(null, sortedList[0]);
			Assert.AreEqual("", sortedList[1]);
			Assert.AreEqual("brown", sortedList[2]);
			Assert.AreEqual("jumps", sortedList[3]);
			Assert.AreEqual("the lazy", sortedList[4]);
		}

		[TestMethod]
		public void StalinSortBy_Int32_NullKeySelector_NotEnumerated() {
			IEnumerable<int> sortedEnumerable = INT_LIST.StalinSortBy<int, int>(null);
			Assert.IsNotNull(sortedEnumerable);
		}

		[TestMethod]
		public void StalinSortBy_Int32_NullKeySelector_Enumerated() {
			ArgumentNullException argumentNullException = Assert.ThrowsException<ArgumentNullException>(() => {
				INT_LIST.StalinSortBy<int, int>(null).ToList();
			});
			Assert.AreEqual("keySelector", argumentNullException.ParamName);
		}
	}
}
