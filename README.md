# StalinSort
Single pass O(n) sort algorithm. You iterate down the list of elements checking if they're in order. Any element which is out of order is eliminated. At the end you have a sorted list.

Don't use this in production.

## Usage
This package provides two extension methods to `IEnumerable<T>`:

```
IEnumerable<TElement> StalinSortBy<TElement, TKey>(Func<TElement, TKey> elementSelector);
IEnumerable<TElement> StalinSortByDescending<TElement, TKey>(Func<TElement, TKey> elementSelector);
```
