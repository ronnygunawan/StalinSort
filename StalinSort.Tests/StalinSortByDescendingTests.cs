using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StalinSort.Tests {

	[TestClass]
	public class StalinSortByDescendingTests {

		private static readonly IReadOnlyList<int> INT_LIST = new List<int> { -5, 1, -1, 0, 2, 3, 4, 80, 80, 70, 60, 50, -100, 1000000, 100, 200, 300 };
		private static readonly IReadOnlyList<string> STRING_LIST = new List<string> { "the", "quick", "", null, "", "brown", "", "fox", "jumps", "over", "the lazy", "DOG" };

		[TestMethod]
		public void StalinSortByDescending_Int32() {
			List<int> sortedList = INT_LIST.StalinSortByDescending(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(2, sortedList.Count);
			Assert.AreEqual(-5, sortedList[0]);
			Assert.AreEqual(-100, sortedList[1]);

			// repeated stalin sort
			sortedList = INT_LIST
				.StalinSortByDescending(element => element)
				.StalinSortByDescending(element => element)
				.StalinSortByDescending(element => element)
				.StalinSortByDescending(element => element)
				.ToList()
				.StalinSortByDescending(element => element)
				.StalinSortByDescending(element => element)
				.ToList();
			Assert.AreEqual(2, sortedList.Count);
			Assert.AreEqual(-5, sortedList[0]);
			Assert.AreEqual(-100, sortedList[1]);

			// 80, 80, 70, 60, 50, 1000000, 100, 200, 300
			sortedList = INT_LIST.Where(element => element > 10).StalinSortByDescending(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(5, sortedList.Count);
			Assert.AreEqual(80, sortedList[0]);
			Assert.AreEqual(80, sortedList[1]);
			Assert.AreEqual(70, sortedList[2]);
			Assert.AreEqual(60, sortedList[3]);
			Assert.AreEqual(50, sortedList[4]);

			// empty sequence
			sortedList = INT_LIST.Where(element => false).StalinSortByDescending(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(0, sortedList.Count);

			// ordered sequence
			sortedList = INT_LIST.OrderByDescending(element => element).StalinSortByDescending(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(INT_LIST.Count, sortedList.Count);
		}

		[TestMethod]
		public void StalinSortByDescending_String() {
			List<string> sortedList = STRING_LIST.StalinSortByDescending(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(4, sortedList.Count);
			Assert.AreEqual("the", sortedList[0]);
			Assert.AreEqual("quick", sortedList[1]);
			Assert.AreEqual("", sortedList[2]);
			Assert.AreEqual(null, sortedList[3]);

			// "" then null
			sortedList = STRING_LIST.Skip(2).StalinSortByDescending(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(2, sortedList.Count);
			Assert.AreEqual("", sortedList[0]);

			// null then ""
			sortedList = STRING_LIST.Skip(3).StalinSortByDescending(element => element).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(1, sortedList.Count);
			Assert.AreEqual(null, sortedList[0]);
		}

		[TestMethod]
		public void StalinSortByDescending_String_ByLength() {
			List<string> sortedList = STRING_LIST.StalinSortByDescending(element => element?.Length).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(3, sortedList.Count);
			Assert.AreEqual("the", sortedList[0]);
			Assert.AreEqual("", sortedList[1]);
			Assert.AreEqual(null, sortedList[2]);

			// "" then null
			sortedList = STRING_LIST.Skip(2).StalinSortByDescending(element => element?.Length).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(2, sortedList.Count);
			Assert.AreEqual("", sortedList[0]);
			Assert.AreEqual(null, sortedList[1]);

			// null then ""
			sortedList = STRING_LIST.Skip(3).StalinSortByDescending(element => element?.Length).ToList();
			Assert.IsNotNull(sortedList);
			Assert.AreEqual(1, sortedList.Count);
			Assert.AreEqual(null, sortedList[0]);
		}

		[TestMethod]
		public void StalinSortByDescending_Int32_NullKeySelector_NotEnumerated() {
			IEnumerable<int> sortedEnumerable = INT_LIST.StalinSortByDescending<int, int>(null);
			Assert.IsNotNull(sortedEnumerable);
		}

		[TestMethod]
		public void StalinSortByDescending_Int32_NullKeySelector_Enumerated() {
			ArgumentNullException argumentNullException = Assert.ThrowsException<ArgumentNullException>(() => {
				INT_LIST.StalinSortByDescending<int, int>(null).ToList();
			});
			Assert.AreEqual("keySelector", argumentNullException.ParamName);
		}
	}
}
