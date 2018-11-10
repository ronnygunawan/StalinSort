using System;
using System.Collections.Generic;

namespace StalinSort {
	public static class IEnumerableExtensions {
		public static IEnumerable<TElement> StalinSortBy<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector) {
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
			IEnumerator<TElement> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext()) {
				TElement element = enumerator.Current;
				TKey maxKey = keySelector(element);
				yield return element;
				Comparer<TKey> comparer = Comparer<TKey>.Default;
				while (enumerator.MoveNext()) {
					element = enumerator.Current;
					TKey key = keySelector(element);
					if (comparer.Compare(key, maxKey) >= 0) {
						yield return element;
						maxKey = key;
					}
				}
			}
			yield break;
		}
		public static IEnumerable<TElement> StalinSortByDescending<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector) {
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
			IEnumerator<TElement> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext()) {
				TElement element = enumerator.Current;
				TKey minKey = keySelector(element);
				yield return element;
				Comparer<TKey> comparer = Comparer<TKey>.Default;
				while (enumerator.MoveNext()) {
					element = enumerator.Current;
					TKey key = keySelector(element);
					if (comparer.Compare(key, minKey) <= 0) {
						yield return element;
						minKey = key;
					}
				}
			}
			yield break;
		}
	}
}
