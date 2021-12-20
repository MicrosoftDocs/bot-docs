---
title: Adaptive expressions prebuilt functions in Bot Framework SDK
description: Learn about the available prebuilt functions in adaptive expressions ordered by their general purpose.
keywords: adaptive expressions, prebuilt functions, reference
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 09/22/2021
monikerRange: 'azure-bot-service-4.0'
---

# Adaptive expressions prebuilt functions

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article lists the available prebuilt functions ordered by their general purpose. For information about operators used in prebuilt functions and expression syntax, see [Operators](../v4sdk/bot-builder-concept-adaptive-expressions.md#operators).

Prebuilt expressions are divided into the following function types:

- [String](#string-functions)
- [Collection](#collection-functions)
- [Logical comparison](#logical-comparison-functions)
- [Conversion](#conversion-functions)
- [Math](#math-functions)
- [Date](#date-and-time-functions)
- [Timex](#timex-functions)
- [URI parsing](#uri-parsing-functions)
- [Object manipulation and construction](#object-manipulation-and-construction-functions)
- [Regular expression](#regular-expression-functions)
- [Type checking](#type-checking-functions)

You can also view the list in [alphabetical order](#prebuilt-functions-sorted-alphabetically).

## String functions

|Function |Explanation|
|-----------|-----------|
|[length](#length)|Return the length of a string.|
|[replace](#replace)|Replace a substring with the specified string and return the updated string. This function is case-sensitive.|
|[replaceIgnoreCase](#replaceIgnoreCase)| Replace a substring with the specified string, and return the updated string. This function is case-insensitive.|
|[split](#split) |Return an array that contains substrings based on the delimiter specified.|
|[substring](#substring)|Return characters from a string.|
|[toLower](#toLower)|Return a string in lowercase in an optional locale format.|
|[toUpper](#toUpper)|Return a string in uppercase in an optional locale format.|
|[trim](#trim)|Remove leading and trailing white spaces from a string.|
|[addOrdinal](#addOrdinal)|Return the ordinal number of the input number.|
|[endsWith](#endsWith)|Check whether a string ends with a specific substring. Return `true` if the substring is found, or return `false` if not found. This function is case-insensitive.|
|[startsWith](#startsWith)|Check whether a string starts with a specific substring. Return `true` if the substring is found, or return `false` if not found. This function is case-insensitive.|
|[countWord](#countWord)|Return the number of words in the given string.|
|[concat](#concat)|Combine two or more strings and return the resulting string.|
|[newGuid](#newGuid)|Return a new Guid string.|
|[indexOf](#indexOf)|Return the starting position or index value of a substring **or** searches for the specified object and return the zero-based index of the first occurrence within the entire list. This function is case-insensitive, and indexes start with the number 0.|
|[lastIndexOf](#lastIndexOf)|Return the starting position or index value of the last occurrence of a substring **or** search for the specified object and return the zero-based index of the last occurrence within the range of elements in the list.This function is case-insensitive, and indexes start with the number 0.|
|[sentenceCase](#sentenceCase)|Capitalize the first letter of the first word in a string in an optional local format.|
|[titleCase](#titleCase)|Capitalize the first letter of each word in a string in an optional locale format.|
|[reverse](#reverse)|Reverse the order of the elements in a string or array.|

## Collection functions

|Function |Explanation|
|-----------|-----------|
|[contains](#contains) |Works to find an item in a string, to find an item in an array, or to find a parameter in a complex object. <br> **Examples**: <br> contains('hello world', 'hello')<br> contains(createArray('1','2'), '1')<br> contains(json("{'foo':'bar'}"), 'foo')|
|[first](#first)|Return the first item from the collection.|
|[join](#join) |Return a string that has all the items from an array and has each character separated by a delimiter.<br>**Example**: <br> join(createArray('a','b'), '.') = "a.b"|
|[last](#last) |Return the last item from the collection.|
|[count](#count)|Return the number of items in the collection.|
|[foreach](#foreach)|Operate on each element and return the new collection.|
|[union](#union)|Return a collection that has all the items from the specified collections.|
|[skip](#skip)|Remove items from the front of a collection, and return the remaining items.|
|[take](#take)|Return items from the front of a collection.|
|[intersection](#intersection)|Return a collection that has only the common items across the specified collections.|
|[subArray](#subArray)|Return a subarray from specified start and end position. Index values start with the number 0.|
|[select](#select)|Operate on each element and return the new collection of transformed elements.|
|[where](#where)|Filter on each element and return the new collection of filtered elements which match the specific condition.|
|[sortBy](#sortBy)|Sort elements in the collection in ascending order and return the sorted collection.|
|[sortByDescending](#sortByDescending)|Sort elements in the collection in descending order and return the sorted collection.|
|[indicesAndValues](#indicesAndValues)|Turn an array or object into an array of objects with index and value property.|
|[flatten](#flatten)|Flatten arrays into an array with non-array values.|
|[unique](#unique)|Remove all duplicates from an array.|
|[any](#any) | Determines whether any elements of a sequence satisfy a condition.|
|[all](#all) | Determine whether all elements of a sequence satisfy a condition.|
|[reverse](#reverse)|Reverse the order of the elements in a string or array.|
|[merge](#merge)| Merges multiple JSON objects or items in an array together.|

## Logical comparison functions

|Function|Explanation|
|-----------|-----------|
|[and](#and)|Logical and. Return true if all specified expressions evaluate to true.|
|[equals](#equals)|Comparison equal. Return true if specified values are equal.|
|[empty](#empty)|Check if the target is empty.|
|[greater](#greater)|Comparison greater than. Return `true` if the first value is more, or return `false` if less.|
|[greaterOrEquals](#greaterOrEquals)|Comparison greater than or equal to. Return `true` if the first value is greater or equal, or return `false` if the first value is less.|
|[if](#if)|Check whether an expression is true or false. Based on the result, return a specified value.|
|[less](#less)| Comparison less than operation. Return `true` if the first value is less, or return `false` if the first value is more.|
|[lessOrEquals](#lessOrEquals)| Comparison less than or equal operation. Return `true` if the first value is less than or equal, or return `false` if the first value is more.|
|[not](#not)| Logical not operator. Return `true` if the expression is false, or return `false` if true.|
|[or](#or)|Logical or operation. Return `true` if at least one expression is true, or return `false` if all are false.|
|[exists](#exists)|Evaluate an expression for truthiness.|

## Conversion functions

|Function|Explanation|
|-----------|-----------|
|[float](#float)|Return the floating point representation of the specified string. |
|[int](#int)|Return the integer representation of the specified string. |
|[string](#string)|Return the string version of the specified value in an optional locale format.|
|[bool](#bool)|Return the Boolean representation of the specified string.|
|[createArray](#createArray)|Create an array from multiple inputs.|
|[json](#json) |Return the JavaScript Object Notation (JSON) type value or object of a string or XML.|
|[base64](#base64)|Return the base64-encoded version of a string or byte array.|
|[base64ToBinary](#base64ToBinary)|Return the binary version for a base64-encoded string.|
|[base64ToString](#base64ToString)|Return the string version of a base64-encoded string.|
|[binary](#binary)|Return the binary version for an input value.|
|[dataUri](#dataUri)|Return the URI for an input value.|
|[dataUriToBinary](#dataUriToBinary)|Return the binary version of a data URI.|
|[dataUriToString](#dataUriToString)|Return the string version of a data URI.|
|[uriComponent](#uriComponent)|Return the URI-encoded version for an input value by replacing URL-unsafe characters with escape characters.|
|[uriComponentToString](#uriComponentToString)|Return the string version of a URI-encoded string.|
|[xml](#xml)|Return the XML version of a string.|
|[formatNumber](#formatNumber)|Format a value to the nearest number to the specified number of fractional digits and an optional specified locale.|
|[jsonStringify](#jsonStringify) | Return the JSON string of a value. |
|[stringOrValue](#stringOrValue) Wrap string interpolation to get the real value. For example, stringOrValue('${1}') returns the number 1, while stringOrValue('${1} item') returns the string "1 item".|

## Math functions

|Function|Explanation|
|-----------|-----------|
|[abs](#abs)| Returns the absolute value of the specified number.|
|[add](#add)|Mathematical and. Return the result from adding two numbers (pure number case) or concatenating two or more strings.|
|[div](#div)|Mathematical division. Return the integer result from dividing two numbers.|
|[max](#max)|Return the largest value from a collection.|
|[min](#min)|Return the smallest value from a collection.|
|[mod](#mod)|Return the remainder from dividing two numbers.|
|[mul](#mul)|Mathematical multiplication. Return the product from multiplying two numbers.|
|[rand](#rand)|Return a random number between specified min and max value.|
|[sqrt](#sqrt)| Return the square root of a specified number. |
|[sub](#sub)|Mathematical subtraction. Return the result from subtracting the second number from the first number.|
|[sum](#sum)|Return the sum of numbers in an array.|
|[range](#range)|Return an integer array that starts from a specified integer.|
|[exp](#exp)|Return exponentiation of one number to another.|
|[average](#average)|Return the average number of an numeric array.|
|[floor](#floor)|Return the largest integral value less than or equal to the specified number.|
|[ceiling](#ceiling)|Return the smallest integral value greater than or equal to the specified number.|
|[round](#round)|Round a value to the nearest integer or to the specified number of fractional digits.|

## Date and time functions

|Function|Explanation|
|-----------|-----------|
|[addDays](#addDays)|Add a number of specified days to a given timestamp in an optional locale format.|
|[addHours](#addHours)|Add a specified number of hours to a given timestamp in an optional locale format.|
|[addMinutes](#addMinutes)|Add a specified number of minutes to a given timestamp in an optional locale format.|
|[addSeconds](#addSeconds)|Add a specified number of seconds to a given timestamp.|
|[dayOfMonth](#dayOfMonth)|Return the day of a month for a given timestamp or Timex expression.|
|[dayOfWeek](#dayOfWeek)|Return the day of the week for a given timestamp.|
|[dayOfYear](#dayOfYear)|Return the day of the year for a given timestamp.|
|[formatDateTime](#formatDateTime)|Return a timestamp in an optional locale format.|
|[formatEpoch](#formatEpoch)|Return a timestamp in an optional locale format from UNIX Epoch time (Unix time, POSIX time).|
|[formatTicks](#formatTicks)|Return a timestamp in an optional locale format from ticks.|
|[subtractFromTime](#subtractFromTime)|Subtract a number of time units from a timestamp in an optional locale format.|
|[utcNow](#utcNow)|Return the current timestamp in an optional locale format as a string.|
|[dateReadBack](#dateReadBack)|Use the date-time library to provide a date readback.|
|[month](#month)|Return the month of given timestamp.|
|[date](#date)|Return the date for a given timestamp.|
|[year](#year)|Return the year for the given timestamp.|
|[getTimeOfDay](#getTimeOfDay)|Return the time of day for a given timestamp. |
|[getFutureTime](#getFutureTime)|Return the current timestamp in an optional locale format plus the specified time units.   |
|[getPastTime](#getPastTime)|Return the current timestamp in an optional locale format minus the specified time units.  |
|[addToTime](#addToTime)   |Add a number of time units to a timestamp in an optional locale format.   |
|[convertFromUTC](#convertFromUTC) |Convert a timestamp in an optional locale format from Universal Time Coordinated (UTC). |
|[convertToUTC](#convertToUTC) |Convert a timestamp  in an optional locale format to Universal Time Coordinated (UTC).  |
|[startOfDay](#startOfDay) |Return the start of the day for a timestamp in an optional locale format.|
|[startOfHour](#startOfHour)   |Return the start of the hour for a timestamp in an optional locale format. |
|[startOfMonth](#startOfMonth) |Return the start of the month for a timestamp in an optional locale format.|
|[ticks](#ticks)   |Return the ticks property value of a specified timestamp.|
|[ticksToDays](#ticksToDays)| Convert a ticks property value to the number of days. |
|[ticksToHours](#ticksToHours)| Convert a ticks property value to the number of hours. |
|[ticksToMinutes](#ticksToMinutes)| Convert a ticks property value to the number of minutes. |
|[dateTimeDiff](#dateTimeDiff)| Return the difference in ticks between two timestamps. |
|[getPreviousViableDate](#getPreviousViableDate) | Return the previous viable date of a Timex expression based on the current date and an optionally specified timezone. |
|[getNextViableDate](#getNextViableDate) | Return the next viable date of a Timex expression based on the current date and an optionally specified timezone. |
|[getPreviousViableTime](#getPreviousViableTime) | Return the previous viable time of a Timex expression based on the current time and an optionally specified timezone. |
|[getNextViableTime](#getNextViableTime) | Return the next viable time of a Timex expression based on the current time and an optionally specified timezone. |

## Timex functions

|Function |Explanation|
|-----------|-----------|
|[isPresent](#isPresent)    | Return true if the TimexProperty or Timex expression refers to the present. |
|[isDuration](#isDuration)    | Return true if the TimexProperty or Timex expression refers to a duration. |
|[isTime](#isTime)    | Return true if the TimexProperty or Timex expression refers to a time. |
|[isDate](#isDate)    | Return true if the TimexProperty or Timex expression refers to a date. |
|[isTimeRange](#isTimeRange)    | Return true if the TimexProperty or Timex expression refers to a time range. |
|[isDateRange](#isDateRange)    | Return true if the TimexProperty or Timex expression refers to a date range. |
|[isDefinite](#isDefinite)    | Return true if the TimexProperty or Timex expression refers to a definite day. |
|[resolve](#resolve)    |    Return a string of a given TimexProperty or Timex expression if it refers to a valid time. Valid time contains hours, minutes, and seconds. |

## URI parsing functions

|Function|Explanation|
|-----------|-----------|
|[uriHost](#uriHost)   |Return the host value of a uniform resource identifier (URI).|
|[uriPath](#uriPath)   |Return the path value of a uniform resource identifier (URI). |
|[uriPathAndQuery](#uriPathAndQuery)  |Return the path and query values for a uniform resource identifier (URI).  |
|[uriPort](#uriPort)   |Return the port value of a uniform resource identifier (URI).|
|[uriQuery](#uriQuery) |Retur0sn the query value of a uniform resource identifier (URI).|
|[uriScheme](#uriScheme)|Return the scheme value of a uniform resource identifier (URI).  |

## Object manipulation and construction functions

|Function|Explanation|
|-----------|-----------|
|[addProperty](#addProperty)   |Add a property and its value, or name-value pair, to a JSON object and return the updated object.|
|[removeProperty](#removeProperty) |Remove a property from JSON object and return the updated object.|
|[setProperty](#setProperty)   |Set the value of a JSON object's property and return the updated object.|
|[getProperty](#getProperty)   |Return the value of a specified property or root property from a JSON object. |
|[coalesce](#coalesce) |Return the first non-null value from one or more parameters. |
|[xPath](#xPath)   |Check XML for nodes or values that match an XPath (XML Path Language) expression, and return the matching nodes or values.|
|[jPath](#jPath)   |Check JSON or a JSON string for nodes or value that match a path expression, and return the matching nodes.|
|[setPathToValue](#setPathToValue)   |Set the value of a specific path and return the value.|

## Regular expression functions

|Function|Explanation|
|-----------|-----------|
|[isMatch](#isMatch)|Return true if a string matches a common regex pattern.|

## Type checking functions

|Function|Explanation|
|-----------|-----------|
|[EOL](#EOL)| Return the end of line (EOL) sequence text.|
|[isInteger](#isInteger)|Return true if  given input is an integer number|
|[isFloat](#isFloat)|Return true if the given input is a float point number|
|[isBoolean](#isBoolean)|Return true if the given input is a Boolean.|
|[isArray](#isArray)|Return true if the given input is an array.|
|[isObject](#isObject)|Return true if the given input is an object.|
|[isDateTime](#isDateTime)|Return true if the given input is a UTC ISO format timestamp.|
|[isString](#isString)|Returns **true** if the given input is a string.|

## Prebuilt functions sorted alphabetically

<a name="abs"></a>

### abs

Return the absolute value of the specified number.

```
abs(<number>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*number*> | Yes | number | Number to get absolute value of |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*result*> | number| The result from computing the absolute value.|
||||

*Examples*

These examples compute the absolute value:

```
abs(3.12134)
abs(-3.12134)
```

And both return the result **3.12134**.

<a name="add"></a>

### add

Return the result from adding two or more numbers (pure number case) or concatenating two or more strings (other case).

```
add(<item1>, <item2>, ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*item1*>, <*item2*>,... | Yes | any | items |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*result-sum*> | number or string | The result from adding the specified numbers or the concat result.|
||||

*Example*

This example adds the specified numbers:

```
add(1, 1.5)
```

And returns the result **2.5**.

This example concatenates the specified items:

```
add('hello',null)
add('hello','world')
```

And returns the results
- **hello**
- **helloworld**

<a name="addDays"></a>

### addDays

Add a number of days to a timestamp in an optional locale format.

```
addDays('<timestamp>', <days>, '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp which must be standard UTC ISO format<br>YYYY-MM-DDTHH:mm:ss.fffZ |
| <*days*> | Yes | integer | The positive or negative number of days to add |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-timestamp*> | string | The timestamp plus the specified number of days  |
||||

*Example 1*

This example adds **10** days to the specified timestamp:

```
addDays('2018-03-15T13:00:00.000Z', 10)
```

And returns the result **2018-03-25T00:00:00.000Z**.

*Example 2*

This example subtracts five days from the specified timestamp:

```
addDays('2018-03-15T00:00:00.000Z', -5)
```

And returns the result **2018-03-10T00:00:00.000Z**.

*Example 3*

This example adds **1** day to the specified timestamp in the **de-DE** locale:

```
addDays('2018-03-15T13:00:00.000Z', 1, '', 'de-dE')
```

And returns the result **16.03.18 13:00:00**.

<a name="addHours"></a>

### addHours

Add a number of hours to a timestamp in an optional locale format.

```
addHours('<timestamp>', <hours>, '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*hours*> | Yes | integer | The positive or negative number of hours to add |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-timestamp*> | string | The timestamp plus the specified number of hours  |
||||

*Example 1*

This example adds **10** hours to the specified timestamp:

```
addHours('2018-03-15T00:00:00.000Z', 10)
```

And returns the result **2018-03-15T10:00:00.000Z**.

*Example 2*

This example subtracts five hours from the specified timestamp:

```
addHours('2018-03-15T15:00:00.000Z', -5)
```

And returns the result **2018-03-15T10:00:00.000Z**.

*Example 3*

This example adds **2** hours to the specified timestamp in the **de-DE** locale:

```
addHours('2018-03-15T13:00:00.000Z', 2, '', 'de-DE')
```

And returns the result **15.03.18 15:00:00**.

<a name="addMinutes"></a>

### addMinutes

Add a number of minutes to a timestamp in an optional locale format.

```
addMinutes('<timestamp>', <minutes>, '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*minutes*> | Yes | integer | The positive or negative number of minutes to add |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-timestamp*> | string | The timestamp plus the specified number of minutes |
||||

*Example 1*

This example adds **10** minutes to the specified timestamp:

```
addMinutes('2018-03-15T00:10:00.000Z', 10)
```

And returns the result  **2018-03-15T00:20:00.000Z**.

*Example 2*

This example subtracts five minutes from the specified timestamp:

```
addMinutes('2018-03-15T00:20:00.000Z', -5)
```

And returns the result **2018-03-15T00:15:00.000Z**.

*Example 3*

This example adds **30** minutes to the specified timestamp in the **de-DE** locale:

```
addMinutes('2018-03-15T00:00:00.000Z', 30, '', 'de-DE')
```

And returns the result **15.03.18 13:30:00**.

<a name="addOrdinal"></a>

### addOrdinal

Return the ordinal number of the input number.

```
addOrdinal(<number>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*number*>| Yes | integer | The numbers to convert to an ordinal number |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*result*> | string | The ordinal number converted from the input number |
||||

*Example*

```
addOrdinal(11)
addOrdinal(12)
addOrdinal(13)
addOrdinal(21)
addOrdinal(22)
addOrdinal(23)
```

And respectively returns these results:
* **11th**
* **12th**
* **13th**
* **21st**
* **22nd**
* **23rd**

<a name="addProperty"></a>

### addProperty

Add a property and its value, or name-value pair, to a JSON object, and return the updated object. If the object already exists at runtime the function throws an error.

```
addProperty('<object>', '<property>', value)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*object*> | Yes | object | The JSON object where you want to add a property |
|<*property*>| Yes | string | The name of the property to add |
|<*value*>| Yes | any | The value of the property |

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-object*> | object | The updated JSON object after adding a new property |
||||

*Example*<br>
This example adds the **accountNumber** property to the **customerProfile** object, which is converted to JSON with the [json()](#json) function. The function assigns a value that is generated by the [newGuid()](#newGuid) function, and returns the updated object:

```
addProperty(json('customerProfile'), 'accountNumber', newGuid())
```

<a name="addSeconds"></a>

### addSeconds

Add a number of seconds to a timestamp.

```
addSeconds('<timestamp>', <seconds>, '<format>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*seconds*> | Yes | integer | The positive or negative number of seconds to add |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-timestamp*> | string | The timestamp plus the specified number of seconds  |
||||

*Example 1*

This example adds 10 seconds to the specified timestamp:

```
addSeconds('2018-03-15T00:00:00.000Z', 10)
```

And returns the result **2018-03-15T00:00:10.000Z**.

*Example 2*

This example subtracts five seconds to the specified timestamp:

```
addSeconds('2018-03-15T00:00:30.000Z', -5)
```

And returns the result **2018-03-15T00:00:25.000Z**.

<a name="addToTime"></a>

### addToTime

Add a number of time units to a timestamp in an optional locale format. See also [getFutureTime()](#getFutureTime).

```
addToTime('<timestamp>', '<interval>', <timeUnit>, '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*interval*> | Yes | integer | The number of specified time units to add |
| <*timeUnit*> | Yes | string | The unit of time to use with *interval*. Possible units are "Second", "Minute", "Hour", "Day", "Week", "Month", and "Year". |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-timestamp*> | string | The timestamp plus the number of specified time units with given format. |
||||

*Example 1*

This example adds one day to specified timestamp.

```
addToTime('2018-01-01T00:00:00.000Z', 1, 'Day')
```

And returns the result **2018-01-02T00:00:00.000Z**.

*Example 2*

This example adds two weeks to the specified timestamp.

```
addToTime('2018-01-01T00:00:00.000Z', 2, 'Week', 'MM-DD-YY')
```

And returns the result in the 'MM-DD-YY' format as **01-15-18**.

<a name="all"></a>

### all

Determine whether all elements of a sequence satisfy a condition.

```
all(<sequence>, <item>, <condition>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*sequence*> | Yes | object | A sequence to be evaluated. |
| <*item*> | Yes | string | Refers to the elements to evaluate in the sequence. |
| <*condition*> | Yes | expression | The expression to evaluate the condition. |
|||||


| Return value | Type | Description |
| ------------ | -----| ----------- |
| true or false | Boolean | Return `true` if all elements satisfy a condition. Return `false` if at least one doesn't. |
||||

*Examples*

These examples determine if all elements of a sequence satisfy a condition:

```
all(createArray(1, 'cool'), item, isInteger(item))
all(createArray(1, 2), item => isInteger(item))
```

And return the following results respectively:
-**false**, because both items in the sequence aren't integers.
-**true**, because both items in the sequence are integers.

<a name="and"></a>

### and

Check whether all expressions are true. Return `true` if all expressions are true, or return `false` if at least one expression is false.

```
and(<expression1>, <expression2>, ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*expression1*>, <*expression2*>, ... | Yes | Boolean | The expressions to check |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| true or false | Boolean | Return `true` if all expressions are true. Return `false` if at least one expression is false. |
||||

*Example 1*

These examples check whether the specified Boolean values are all true:

```
and(true, true)
and(false, true)
and(false, false)
```

And respectively returns these results:

- Both expressions are true, so the functions returns `true`.
- One expression is false, so the functions returns `false`.
- Both expressions are false, so the function returns `false`.

*Example 2*

These examples check whether the specified expressions are all true:

```
and(equals(1, 1), equals(2, 2))
and(equals(1, 1), equals(1, 2))
and(equals(1, 2), equals(1, 3))
```

And respectively returns these results:

* Both expressions are true, so the functions returns `true`.
* One expression is false, so the functions returns `false`.
* Both expressions are false, so the functions returns `false`.

<a name="any"></a>

### any

Determine whether any elements of a sequence satisfy a condition.

```
all(<sequence>, <item>, <condition>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*sequence*> | Yes | object | A sequence to be evaluated. |
| <*item*> | Yes | string | Refers to the elements to evaluate in the sequence. |
| <*condition*> | Yes | expression | The expression to evaluate the condition. |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| true or false | Boolean | Return `true` if all elements satisfy the condition. Return `false` if at least one doesn't. |
||||

*Examples*

These examples determine if all elements of a sequence satisfy a condition:

```
any(createArray(1, 'cool'), item, isInteger(item))
any(createArray('first', 'cool'), item => isInteger(item))
```

And return the following results respectively:
-**true**, because at least one item in the sequence is an integer
-**false**, because neither item in the sequence is an integer.

<a name="average"></a>

### average

Return the number average of a numeric array.

```
average(<numericArray>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*numericArray*> | Yes | array of number | The input array to calculate the average |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*average-of-array*> | number | The average value of the given array |
||||

*Example*

This example calculates the average of the array in `createArray()`:

```
average(createArray(1,2,3))
```

And returns the result **2**.

<a name="base64"></a>

### base64

Return the base64-encoded version of a string or byte array.

```
base64('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string or byte array | The input string |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*base64-string*> | string | The base64-encoded version of the input string |
||||

*Example 1*

This example converts the string **hello** to a base64-encoded string:

```
base64('hello')
```

And returns the result **"aGVsbG8="**.

*Example 2*

This example takes `byteArr`, which equals `new byte[] { 3, 5, 1, 12 }`:

```
base64('byteArr')
```

And returns the result **"AwUBDA=="**.

<a name="base64ToBinary"></a>

### base64ToBinary

Return the binary array of a base64-encoded string.

```
base64ToBinary('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string | The base64-encoded string to convert |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*binary-for-base64-string*> | byte array | The binary version of the base64-encoded string |
||||

*Example*

This example converts the base64-encoded string **AwUBDA==** to a binary string:

```
base64ToBinary('AwUBDA==')
```

And returns the result **new byte[] { 3, 5, 1, 12 }**.

<a name="base64ToString"></a>

### base64ToString

Return the string version of a base64-encoded string, effectively decoding the base64 string.

```
base64ToString('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string | The base64-encoded string to decode |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*decoded-base64-string*> | string | The string version of a base64-encoded string |
||||

*Example*

This example converts the base64-encoded string **aGVsbG8=** to a decoded string:

```
base64ToString('aGVsbG8=')
```

And returns the result **hello**.

<a name="binary"></a>

### binary

Return the binary version of a string.

```
binary('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string | The string to convert |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*binary-for-input-value*> | byte array | The binary version of the specified string |
||||

*Example*

This example converts the string **hello** to a binary string:

```
binary('hello')
```

And returns the result **new byte[] { 104, 101, 108, 108, 111 }**.

<a name="bool"></a>

### bool

Return the Boolean version of a value.

```
bool(<value>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | any | The value to convert |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | The Boolean version of the specified value |
||||

*Example*

These examples convert the specified values to Boolean values:

```
bool(1)
bool(0)
```

And respectively returns these results:

* `true`
* `false`

<a name="ceiling"></a>

### ceiling

Return the largest integral value less than or equal to the specified number.

```
ceiling('<number>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*number*> | Yes | number | An input number |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*integer-value*> | integer | The largest integral value greater than or equal to the input number |
||||

*Example*

This example returns the largest integral value less than or equal to the number **10.333**:

```
ceiling(10.333)
```

And returns the integer **11**.

<a name="coalesce"></a>

### coalesce

Return the first non-null value from one or more parameters. Empty strings, empty arrays, and empty objects are not null.

```
coalesce(<object**1>, <object**2>, ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*object**1*>, <*object**2*>, ... | Yes | any (mixed types acceptable) | One or more items to check for null|
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*first-non-null-item*> | any | The first item or value that is not null. If all parameters are null, this function returns null. |
||||

*Example*

These examples return the first non-null value from the specified values, or null when all the values are null:

```
coalesce(null, true, false)
coalesce(null, 'hello', 'world')
coalesce(null, null, null)
```

And respectively return:

- `true`
- **hello**
- null

<a name="concat"></a>

### concat

Combine two or more objects, and return the combined objects in a list or string.

```
concat('<text1>', '<text2>', ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*object1*>, <*object2*>, ... | Yes | any | At least two objects to concat. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*object1object2...*> | string or list | The combined string or list. Null values are skipped. |
||||

Expected return values:

- If all items are lists, a list will be returned.
- If there exists an item that is not a list, a string will be returned.
- If a value is null, it is skipped and not concatenated.

*Example*

This example combines the strings **Hello** and **World**:

```
concat('Hello', 'World')
```

And returns the result **HelloWorld**.

*Example 2*

This example combines the lists **[1,2]** and **[3,4]**:

```
concat([1,2],[3,4])
```

And returns the result **[1,2,3,4]**.

*Example 3*

These examples combine objects of different types:

```
concat('a', 'b', 1, 2)
concat('a', [1,2])
```

And return the following results respectively:

- The string **ab12**.
- The object **aSystem.Collections.Generic.List 1[System.Object]**. This is unreadable and best to avoid.

*Example 4*

These examples combine objects will `null`:

```
concat([1,2], null)
concat('a', 1, null)
```

And return the following results respectively:

- The list **[1,2]**.
- The string **a1**.

<a name="contains"></a>

### contains

Check whether a collection has a specific item. Return `true` if the item is found, or return `false` if not found. This function is case-sensitive.

```
contains('<collection>', '<value>')
contains([<collection>], '<value>')
```

This function works on the following collection types:

* A string to find a substring
* An array to find a value
* A dictionary to find a key

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | string, array, or dictionary | The collection to check |
| <*value*> | Yes | string, array, or dictionary, respectively | The item to find |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` if the item is found. Return `false` if not found. |
||||

*Example 1*

This example checks the string **hello world** for the substring **world**:

```
contains('hello world', 'world')
```

And returns the result `true`.

*Example 2*

This example checks the string **hello world** for the substring **universe**:

```
contains('hello world', 'universe')
```

And returns the result `false`.

<a name="count"></a>

### count

Return the number of items in a collection.

```
count('<collection>')
count([<collection>])
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | string or array | The collection with the items to count |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*length-or-count*> | integer | The number of items in the collection |
||||

*Examples*:

These examples count the number of items in these collections:

```
count('abcd')
count(createArray(0, 1, 2, 3))
```

And both return the result **4**.

<a name="countWord"></a>

### countWord

Return the number of words in a string

```
countWord('<text>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string to count |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*count*> | integer | The number of words in the string |
||||

*Example*

This example counts the number of words in the string **hello world**:

```
countWord("hello word")
```

And it returns the result **2**.

<a name="convertFromUTC"></a>

### convertFromUTC

Convert a timestamp in an optional locale format from Universal Time Coordinated (UTC) to a target time zone.

```
convertFromUTC('<timestamp>', '<destinationTimeZone>', '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*destinationTimeZone*> | Yes | string | The name of the target time zone. Supports Windows and IANA time zones. |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is the ["o" format](/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip), yyyy-MM-ddTHH:mm:ss.fffffffK, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*converted-timestamp*> | string | The timestamp converted to the target time zone |
||||

*Examples*:

These examples convert from UTC to Pacific Standard Time:

```
convertFromUTC('2018-02-02T02:00:00.000Z', 'Pacific Standard Time', 'MM-DD-YY')
convertFromUTC('2018-02-02T02:00:00.000Z', 'Pacific Standard Time')
```

And respectively return these results:

* **02-01-18**
* **2018-01-01T18:00:00.0000000**

*Example 2*

This example converts a timestamp in the **en-US** locale from UTC to Pacific Standard Time:

```
convertFromUTC('2018-01-02T02:00:00.000Z', 'Pacific Standard Time', 'D', 'en-US')
```

And returns the result **Monday, January 1, 2018**.

<a name="convertToUTC"></a>

### convertToUTC

Convert a timestamp in an optional locale format to Universal Time Coordinated (UTC) from the source time zone.

```
convertToUTC('<timestamp>', '<sourceTimeZone>', '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*sourceTimeZone*> | Yes | string | The name of the target time zone. Supports Windows and IANA time zones. |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*converted-timestamp*> | string | The timestamp converted to the target time zone |
||||

*Example*

This example converts a timestamp to UTC from Pacific Standard Time

```
convertToUTC('01/01/2018 00:00:00', 'Pacific Standard Time')
```

And returns the result **2018-01-01T08:00:00.000Z**.

*Example 2*

This example converts a timestamp in the **de-DE** locale to UTC from Pacific Standard Time:

```
convertToUTC('01/01/2018 00:00:00', 'Pacific Standard Time', '', 'de-DE')
```

And returns the result **01.01.18 08:00:00**.

<a name="createArray"></a>

### createArray

Return an array from multiple inputs.

```
createArray('<object1>', '<object2>', ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*object1*>, <*object2*>, ... | Yes | any, but not mixed | At least two items to create the array |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| [<*object1*>, <*object2*>, ...] | array | The array created from all the input items |
||||

*Example*

This example creates an array from the following inputs:

```
createArray('h', 'e', 'l', 'l', 'o')
```

And returns the result **[h ,e, l, l, o]**.

<a name="dataUri"></a>

### dataUri

Return a data uniform resource identifier (URI) of a string.

```
dataUri('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*>| Yes | string | The string to convert |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| [<*date-uri*>] | string | The data URI for the input string |
||||

*Example*

```
dataUri('hello')
```

Returns the result **data:text/plain;charset=utf-8;base64,aGVsbG8=**.

<a name="dataUriToBinary"></a>

### dataUriToBinary

Return the binary version of a data uniform resource identifier (URI).

```
dataUriToBinary('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*>| Yes | string | The data URI to convert |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| [<*binary-for-data-uri*>] | byte array | The binary version of the data URI |
||||

*Example*

This example creates a binary version for the following data URI:

```
dataUriToBinary('aGVsbG8=')
```

And returns the result **new byte[] { 97, 71, 86, 115, 98, 71, 56, 61 }**.

<a name="dataUriToString"></a>

### dataUriToString

Return the string version of a data uniform resource identifier (URI).

```
dataUriToString('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*>| Yes | string | The data URI to convert |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| [<*string-for-data-uri*>] | string | The string version of the data URI |
||||

*Example*

This example creates a string from the following data URI:

```
dataUriToString('data:text/plain;charset=utf-8;base64,aGVsbG8=')
```

And returns the result **hello**.

<a name="date"></a>

### date

Return the date of a specified timestamp in **m/dd/yyyy** format.

```
date('<timestramp>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*date*> | string | The date of the specified timestamp |
||||

```
date('2018-03-15T13:00:00.000Z')
```

Returns the result **3-15-2018**.

<a name="dateReadBack"></a>

### dateReadBack

Uses the date-time library to provide a date readback.

```
dateReadBack('<currentDate>', '<targetDate>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*currentDate*> | Yes | string | The string that contains the current date |
| <*targetDate*> | Yes | string | The string that contains the target date |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*date-readback*> | string | The readback between current date and the target date  |
||||

*Example 1*

```
dateReadBack('2018-03-15T13:00:00.000Z', '2018-03-16T13:00:00.000Z')
```

Returns the result **tomorrow**.

<a name="dateTimeDiff"></a>

### dateTimeDiff

Return the difference in ticks between two timestamps.

```
dateTimeDiff('<timestamp1>', '<timestamp2>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp1*> | Yes | string | The first timestamp string to compare |
| <*timestamp2*> | Yes | string | The second timestamp string to compare |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*ticks*> | number | The difference in ticks between two timestamps  |
||||

*Example 1*

This example returns the difference in ticks between two timestamps:

```
dateTimeDiff('2019-01-01T08:00:00.000Z','2018-01-01T08:00:00.000Z')
```

And returns the number **315360000000000**.

*Example 2*

This example returns the difference in ticks between two timestamps:

```
dateTimeDiff('2018-01-01T08:00:00.000Z', '2019-01-01T08:00:00.000Z')
```

Returns the result **-315360000000000**. The value is a negative number.

<a name="dayOfMonth"></a> <a name="dayOfMonth"></a>

### dayOfMonth

Return the day of the month from a timestamp.

```
dayOfMonth('<timestamp>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*day-of-month*> | integer | The day of the month from the specified timestamp |
||||

*Example*

This example returns the number for the day of the month from the following timestamp:

```
dayOfMonth('2018-03-15T13:27:36Z')
```

And returns the result **15**.

<a name="dayOfWeek"></a>

### dayOfWeek

Return the day of the week from a timestamp.

```
dayOfWeek('<timestamp>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*day-of-week*> | integer | The day of the week from the specified timestamp. Sunday is 0, Monday is 1, and so forth.  |
||||

*Example*

This example returns the number for the day of the week from the following timestamp:

```
dayOfWeek('2018-03-15T13:27:36Z')
```

And returns the result **3**.

<a name="dayOfYear"></a>

### dayOfYear

Return the day of the year from a timestamp.

```
dayOfYear('<timestamp>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*day-of-year*> | integer | The day of the year from the specified timestamp |
||||

*Example*

This example returns the number of the day of the year from the following timestamp:

```
dayOfYear('2018-03-15T13:27:36Z')
```

And returns the result **74**.

<a name="div"></a>

### div

Return the integer result from dividing two numbers. To return the remainder see [mod()](#mod).

```
div(<dividend>, <divisor>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*dividend*> | Yes | number | The number to divide by the *divisor* |
| <*divisor*> | Yes | number | The number that divides the *dividend*. Cannot be 0. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*quotient-result*> | number | The result from dividing the first number by the second number |
||||

*Example*

Both examples divide the first number by the second number:

```
div(10, 5)
div(11, 5)
```

And return the result **2**.

If one of the parameters is a float, the result will also be a float.

*Example*

```
div(11.2, 2)
```

Returns the result **5.6**.

<a name="empty"></a>

### empty

Check whether an instance is empty. Return `true` if the input is empty.
Empty means:

- input is null or undefined
- input is a null or empty string
- input is zero size collection
- input is an object with no property.

```
empty('<instance>')
empty([<instance>])
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*instance*> | Yes | any | The instance to check |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` when the instance is empty.|
||||

*Example*

These examples check whether the specified instance is empty:

```
empty('')
empty('abc')
empty([1])
empty(null)
```

And return these results respectively:

* Passes an empty string, so the function returns `true`.
* Passes the string **abc**, so the function returns `false`.
* Passes the collection with one item, so the function returns `false`.
* Passes the null object, so the function returns `true`.

<a name="endsWith"></a>

### endsWith

Check whether a string ends with a specific substring. Return `true` if the substring is found, or return `false` if not found. This function is case-insensitive.

```
endsWith('<text>', '<searchText>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string to check |
| <*searchText*> | Yes | string | The ending substring to find |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` when the ending substring is found. Return `false` if not found |
||||

*Example 1*

This example checks whether the **hello world** string ends with the string **world**:

```
endsWith('hello world', 'world')
```

And it returns the result `true`.

*Example 2*

This example checks whether the **hello world** string ends with the string **universe**:

```
endsWith('hello world', 'universe')
```

And it returns the result `false`.

<a name="EOL"></a>

### EOL

Return the end of line (EOL) sequence text.

```
EOL()
```

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*IsOSPlatform*>| string | Return **\r\n** in Windows and **\n** in Mac and Linux. |
||||

*Example*

This example checks the end of the line sequence text:

```
EOL()
```

And returns the following strings:

- Windows: **\r\n**
- Mac or Linux: **\n**

<a name="equals"></a>

### equals

Check whether both values, expressions, or objects are equivalent. Return `true` if both are equivalent, or return `false` if they're not equivalent.

```
equals('<object1>', '<object2>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*object1*>, <*object2*> | Yes | any | The values, expressions, or objects to compare |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` when both are equivalent. Return `false` if not equivalent. |
||||

*Example*

These examples check whether the specified inputs are equivalent:

```
equals(true, 1)
equals('abc', 'abcd')
```

And returns these results respectively:

* Both values are equivalent, so the function returns `true`.
* Both values aren't equivalent, so the function returns `false`.

<a name="exists"></a>

### exists

Evaluates an expression for truthiness.

```
exists(expression)
```

| Parameter | Required | Type | Description |
|-----------|----------|------|-------------|
| expression | Yes | expression | Expression to evaluate for truthiness |
|||||

| Return value | Type | Description |
|--------------|------|-------------|
| <*true or false*> | Boolean | Result of evaluating the expression |

*Example*

These example evaluate the truthiness of `foo = {"bar":"value"}`:

```
exists(foo.bar)
exists(foo.bar2)
```

And return these results respectively:
- `true`
- `false`

<a name="exp"></a>

### exp

Return exponentiation of one number to another.

```
exp(realNumber, exponentNumber)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| realNumber | Yes | number | Real number to compute exponent of |
| exponentNumber | Yes | number | Exponent number |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*result-exp*> | number | The result from computing exponent of `realNumber` |
||||

*Example*

This example computes the exponent:

```
exp(2, 2)
```

And returns the result **4**.

<a name="first"></a>

### first

Return the first item from a string or array.

```
first('<collection>')
first([<collection>])
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | string or array | The collection in which to find the first item |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*first-collection-item*> | any | The first item in the collection |
||||

*Example*

These examples find the first item in the following collections:

```
first('hello')
first(createArray(0, 1, 2))
```

And return these results respectively:
* **h**
* **0**

<a name="flatten"></a>

### flatten

Flatten an array into non-array values.  You can optionally set the maximum depth to flatten to.

```
flatten([<collection>], '<depth>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | array | The collection to flatten |
| <*depth*> | No | number | Maximum depth to flatten. Default is infinity. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*new-collection*> | array | New collection whose elements have been flattened to a non-array to the specified depth |
||||

*Example 1*

THis example flattens the following array:

```
flatten(createArray(1, createArray(2), createArray(createArray(3, 4), createArray(5, 6)))
```

And returns the result **[1, 2, 3, 4, 5, 6]**.

*Example 2*

This example flattens the array to a depth of **1**:

```
flatten(createArray(1, createArray(2), createArray(createArray(3, 4), createArray(5, 6)), 1)
```

And returns the result **[1, 2, [3, 4], [5, 6]]**.

<a name="float"></a>

### float

Convert the string version of a floating-point number to a floating-point number. You can use this function only when passing custom parameters to an app, such as a logic app. An exception will be thrown if the string cannot be converted to a float.

```
float('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string | The string that has a valid floating-point number to convert to |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*float-value*> | float | The floating-point number of the specified string |
||||

*Example*

This example converts the float version of a string:

```
float('10.333')
```

And returns the float **10.333**.

<a name="floor"></a>

### floor

Return the largest integral value less than or equal to the specified number.

```
floor('<number>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*number*> | Yes | number | An input number |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*integer-value*> | integer | The largest integral value less than or equal to the input number |
||||

*Example*

This example calculates the floor value of the number **10.333**:

```
floor(10.333)
```

And returns the integer **10**.

<a name="foreach"></a>

### foreach

Operate on each element and return the new collection.

```
foreach([<collection/instance>], <iteratorName>, <function>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection/instance*> | Yes | array or object | The collection with the items |
| <*iteratorName*> | Yes | iterator name | The key item of arrow function |
| <*function*> | Yes | expression | Function that contains `iteratorName` |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*new-collection*> | array | The new collection in which each element has been evaluated by the function  |
||||

*Example 1*

This example generates a new collection:

```
foreach(createArray(0, 1, 2, 3), x, x + 1)
```

And returns the result **[1, 2, 3, 4]**.

*Example 2*

These examples generate a new collection:

```
foreach(json("{'name': 'jack', 'age': '15'}"), x, concat(x.key, ':', x.value))
foreach(json("{'name': 'jack', 'age': '15'}"), x=> concat(x.key, ':', x.value))
```

And return the result **['name:jack', 'age:15']**. Note that the second expression is a *lambda expression*, which some find more readable.

<a name="formatDateTime"></a>

### formatDateTime

Return a timestamp in an optional locale format.

```
formatDateTime('<timestamp>', '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||


| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*reformatted-timestamp*> | string | The updated timestamp in the specified format |
||||

*Example 1*

This example converts a timestamp to the specified format:

```
formatDateTime('03/15/2018 12:00:00', 'yyyy-MM-ddTHH:mm:ss')
```

And returns the result **2018-03-15T12:00:00**.

*Example 2*

This example converts a timestamp in the **de-DE** locale:

```
formatDateTime('2018-03-15', '', 'de-DE')
```

And returns the result **15.03.18 00:00:00**.

<a name="formatEpoch"></a>

### formatEpoch

Return a timestamp in an optional locale format in the specified format from UNIX time (also know as Epoch time, POSIX time, UNIX Epoch time).

```
formatEpoch('<epoch>', '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*epoch*> | Yes | number | The epoch number |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*reformatted-timestamp*> | string | The updated timestamp in the specified format |
||||

*Example*

This example converts a Unix timestamp to the specified format:

```
formatEpoch(1521118800, 'yyyy-MM-ddTHH:mm:ss.fffZ)'
```

And returns the result **2018-03-15T12:00:00.000Z**.

*Example*

This example converts a Unix timestamp in the **de-DE** locale:

```
formatEpoch(1521118800, '', 'de-DE')
```

And returns the result **15.03.18 13:00:00**.

<a name="formatNumber"></a>

### formatNumber

Format a value to the specified number of fractional digits and an optional specified locale.

```
formatNumber('<number>', '<precision-digits>', '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*number*> | Yes | number | An input number |
| <*precision-digits*> | Yes | integer | A specified number of fractional digits|
| <*locale*> | No| string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*return-value*> | number | The return value of the input formatted at a specified number of fractional digits and a specified locale |
||||

*Example 1*

This example formats the number **10.333** to **2** fractional digits:

```
formatNumber(10.333, 2)
```

And returns the string **10.33**.

*Example 2*

These examples format numbers to a specified number of digits in the **en-US** locale:

```
formatNumber(12.123, 2, 'en-US')
formatNumber(1.551, 2, 'en-US')
formatNumber(12.123, 4, 'en-US')
```

And return the following results respectively:

- **12.12**
- **1.55**
- **12.1230**


<a name="formatTicks"></a>

### formatTicks

Return a timestamp in an optional locale format in the specified format from ticks.

```
formatTicks('<ticks>', '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*epoch*> | Yes | number (or bigint in JavaScript)| The ticks number |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*reformatted-timestamp*> | string | The updated timestamp in the specified format |
||||

*Example 1*

This example converts ticks to the specified format:

```
formatTicks(637243624200000000, 'yyyy-MM-ddTHH:mm:ss.fffZ')
```

And returns the result **2020-05-06T11:47:00.000Z**.

*Example 2*

This example converts ticks to the specified format in the **de-DE** locale:

```
formatTicks(637243624200000000, '', 'de-DE')
```

And returns the result **06.05.20 11:47:00**.

<a name="getFutureTime"></a>

### getFutureTime

Return the current timestamp in an optional locale format plus the specified time units.

```
getFutureTime(<interval>, <timeUnit>, '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*interval*> | Yes | integer | The number of specific time units to add |
| <*timeUnit*> | Yes | string | The unit of time to use with *interval*. Possible units are "Second", "Minute", "Hour", "Day", "Week", "Month", and "Year".|
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-timestamp*> | string | The current timestamp plus the specified number of time units |
||||

*Example 1*

Suppose the current timestamp is **2019-03-01T00:00:00.000Z**. The example below adds five days to that timestamp:

```
getFutureTime(2, 'Week')
```

And returns the result **2019-03-15T00:00:00.000Z**.

*Example 2*

Suppose the current timestamp is **2018-03-01T00:00:00.000Z**. The example below adds five days to the timestamp and converts the result to **MM-DD-YY** format:

```
getFutureTime(5, 'Day', 'MM-DD-YY')
```

And returns the result **03-06-18**.

*Example 3*

Suppose the current timestamp is **2020-05-01T00:00:00.000Z** and the locale is **de-DE**. The example below adds **1** day to the timestamp:

```
getFutureTime(1,'Day', '', 'de-DE')
```

And returns the result **02.05.20 00:00:00**.

<a name="getNextViableDate"></a>

### getNextViableDate

Return the next viable date of a Timex expression based on the current date and an optionally specified timezone.

```
getNextViableDate(<timexString>, <timezone>?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timexString*> | Yes | string | The Timex string of the date to evaluate. |
| <*timezone*> | No | string | The optional timezone. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*nextViableTime*> | string | The next viable date. |
||||

*Examples*

Say the date is **2020-06-12** and current time is **15:42:21**.

These examples evaluate the Timex string for the next viable date based on the above date and time:

```
getPreviousViableDate("XXXX-12-20", "America/Los_Angeles")
getPreviousViableDate("XXXX-02-29")
```

And return the following strings respectively:

- **2020-12-20**
- **2024-02-29**

<a name="getNextViableTime"></a>

### getNextViableTime

Return the next viable time of a Timex expression based on the current time and an optionally specified timezone.

```
getNextViableTime(<timexString>, <timezone>?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timexString*> | Yes | string | The Timex string of the time to evaluate. |
| <*timezone*> | No | string | The optional timezone. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*nextViableTime*> | string | The next viable time. |
||||

*Examples*

Say the date is **2020-06-12** and current time is **15:42:21**.

These examples evaluate a Timex string for the next viable time based on the above date and time:

```
getNextViableTime("TXX:12:14", "Asia/Tokyo")
getNextViableTime("TXX:52:14")
```

And return the following strings respectively:

- **T16:12:14**
- **T15:52:14**

<a name="getPastTime"></a>

### getPastTime

Return the current timestamp minus the specified time units.

```
getPastTime(<interval>, <timeUnit>, '<format>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*interval*> | Yes | integer | The number of specific time units to subtract |
| <*timeUnit*> | Yes | string | The unit of time to use with *interval*. Possible units are "Second", "Minute", "Hour", "Day", "Week", "Month", and "Year". |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-timestamp*> | string | The current timestamp minus the specified number of time units |
||||

*Example 1*

Suppose the current timestamp is **2018-02-01T00:00:00.000Z**. This example subtracts five days from that timestamp:

```
getPastTime(5, 'Day')
```

And returns the result **2019-01-27T00:00:00.000Z**.

*Example 2*

Suppose the current timestamp is **2018-03-01T00:00:00.000Z**. This example subtracts five days to the timestamp in the **MM-DD-YY** format:

```
getPastTime(5, 'Day', 'MM-DD-YY')
```

And returns the result **02-26-18**.

*Example 3*

Suppose the current timestamp is **2020-05-01T00:00:00.000Z** and the locale is **de-DE**. The example below subtracts **1** day from the timestamp:

```
getPastTime(1,'Day', '', 'de-DE')
```

And returns the result **31.04.20 00:00:00**.

<a name="getPreviousViableDate"></a>

### getPreviousViableDate

Return the previous viable date of a Timex expression based on the current date and an optionally specified timezone.

```
getPreviousViableDate(<timexString>, <timezone>?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timexString*> | Yes | string | The Timex string of the date to evaluate. |
| <*timezone*> | No | string | The optional timezone. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*previousViableDate*> | string | The previous viable date. |
||||

*Examples*

Say the date is **2020-06-12** and current time is **15:42:21**.

These examples evaluate a Timex string for the previous viable date based on the above date and time:

```
getPreviousViableDate("XXXX-12-20", "Eastern Standard Time")
getPreviousViableDate("XXXX-02-29")
```

And return the following strings respectively:

- **2019-12-20**
- **2020-02-29**

<a name="getPreviousViableTime"></a>

### getPreviousViableTime

Return the previous viable time of a Timex expression based on the current date and an optionally specified timezone.

```
getPreviousViableTime(<timexString>, <timezone>?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timexString*> | Yes | string | The Timex string of the time to evaluate. |
| <*timezone*> | No | string | The optional timezone. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*previousViableTime*> | string | The previous viable time. |
||||

*Examples*

Say the date is **2020-06-12** and current time is **15:42:21**.

These examples evaluate a Timex string for the previous viable time based on the above date and time:

```
getPreviousViableTime("TXX:52:14")
getPreviousViableTime("TXX:12:14", 'Europe/London')
```

And return the following strings respectively:

- **T14:52:14**
- **T15:12:14**

<a name="getProperty"></a>

### getProperty

Return the value of a specified property or the root property from a JSON object.

#### Return the value of a specified property

```
getProperty(<JSONObject>, '<propertyName>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*JSONObject*> | Yes | object | The JSON object containing the property and values. |
| <*propertyName*> | No | string | The name of the optional property to access values from.|
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| value | string | The value of the specified property in the JSON object. |
||||

*Example*

Say you have the following JSON object:

```json
{
   "a:b" : "a:b value",
   "c":
   {
        "d": "d key"
    }
}
```

These example retrieve a specified property from the above JSON object:

```
getProperty({"a:b": "value"}, 'a:b')
getProperty(c, 'd')
```

And return the following strings respectively:

- **a:b value**
- **d key**

#### Return the root property

```
getProperty('<propertyName>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*propertyName*> | Yes | string | The name of the optional property to access values from the root memory scope. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| value | string | The value of the root property in a JSON object. |
||||

*Example*

Say you have the following JSON object:

```json
{
   "a:b" : "a:b value",
   "c":
   {
        "d": "d key"
    }
}
```

This example retrieves the root property from the above JSON object:

```
getProperty("a:b")
```

And returns the string **a:b value**.

<a name="getTimeOfDay"></a>

### getTimeOfDay

Returns time of day for a given timestamp.

```
getTimeOfDay('<timestamp>')
```

Time returned is one of the following strings:


| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the specified timestamp |

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*time-of-day*> | string | The time of day for the specified timestamp|
||||

Listed below are the strings associated with the time of day:

|Time of day | Timestamp |
|---|---|
| midnight | 12AM |
| morning | 12:01AM – 11:59PM |
| afternoon | 12PM |
| evening | 06:00PM – 10:00PM |
| night | 10:01PM – 11:59PM |


*Example*

```
getTimeOfDay('2018-03-15T08:00:00.000Z')
```

Returns the result **morning**.

<a name="greater"></a>

### greater

Check whether the first value is greater than the second value. Return `true` if the first value is more, or return `false` if less.

```
greater(<value>, <compareTo>)
greater('<value>', '<compareTo>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | integer, float, or string | The first value to check whether greater than the second value |
| <*compareTo*> | Yes | integer, float, or string, respectively  | The comparison value |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` if the first value is greater than the second value. Return `false` if the first value is equal to or less than the second value. |
||||

*Example*

These examples check whether the first value is greater than the second value:

```
greater(10, 5)
greater('apple', 'banana')
```

And return the following results respectively:

* `true`
* `false`

<a name="greaterOrEquals"></a>

### greaterOrEquals

Check whether the first value is greater than or equal to the second value. Return `true` when the first value is greater or equal, or return `false` if the first value is less.

```
greaterOrEquals(<value>, <compareTo>)
greaterOrEquals('<value>', '<compareTo>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | integer, float, or string | The first value to check whether greater than or equal to the second value |
| <*compareTo*> | Yes | integer, float, or string, respectively | The comparison value |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` if the first value is greater than or equal to the second value. Return `false` if the first value is less than the second value. |
||||

*Example*

These examples check whether the first value is greater or equal than the second value:

```
greaterOrEquals(5, 5)
greaterOrEquals('apple', 'banana')
```

And return the following results respectively:

* `true`
* `false`

<a name="if"></a>

### if

Check whether an expression is true or false. Based on the result, return a specified value.

```
if(<expression>, <valueIfTrue>, <valueIfFalse>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*expression*> | Yes | Boolean | The expression to check |
| <*valueIfTrue*> | Yes | any | The value to return if the expression is true |
| <*valueIfFalse*> | Yes | any | The value to return if the expression is false |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*specified-return-value*> | any | The specified value that returns based on whether the expression is `true` or `false` |
||||

*Example*

This example evaluates whether `equals(1,1)` is true:

```
if(equals(1, 1), 'yes', 'no')
```

And returns **yes** because the specified expression returns `true`. Otherwise, the example returns **no**.


<a name="indexOf"></a>

### indexOf

Return the starting position or index value of a substring. This function is case-insensitive, and indexes start with the number 0.

```
indexOf('<text>', '<searchText>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string or array | The string that has the substring to find |
| <*searchText*> | Yes | string | The substring to find |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*index-value*> | integer | The starting position or index value of the specified substring.
If the string is not found, return the number -1. |
||||

*Example 1*

This example finds the starting index value of the substring **world** in the string **hello world**:

```
indexOf('hello world', 'world')
```

And returns the result **6**.

*Example 2*

This example finds the starting index value of the substring **def** in the array **['abc', 'def', 'ghi']**:
```
indexOf(createArray('abc', 'def', 'ghi'), 'def')
```

And returns the result **1**.

<a name="indicesAndValues"></a>

### indicesAndValues

Turn an array or object into an array of objects with index (current index) and value properties. For arrays, the index is the position in the array.  For objects, it is the key for the value.

```
indicesAndValues('<collection or object>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection or object*> | Yes | array or object | Original array or object |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*collection*> | array | New array. Each item has two properties: the index with the position in an array or the key for an object, and the corresponding value. |
||||

*Example 1*

Say you have a list **{ items: ["zero", "one", "two"] }**. The following function takes that list:

```
indicesAndValues(items)
```

And returns a new list:

```
[
  {
    index: 0,
    value: 'zero'
  },
  {
    index: 1,
    value: 'one'
  },
  {
    index: 2,
    value: 'two'
  }
]
```

*Example 2*

Say you have a list **{ items: ["zero", "one", "two"] }**. The following function takes that list:

```
where(indicesAndValues(items), elt, elt.index >= 1)
```

And returns a new list:
```
[
  {
    index: 1,
    value: 'one'
  },
  {
    index: 2,
    value: 'two'
  }
]
```

*Example 3*

Say you have a list **{ items: ["zero", "one", "two"] }**. The following function takes that list:

```
join(foreach(indicesAndValues(items), item, item.value), ',')
```

And returns the result **zero,one,two**. This expression has the same effect as [join(items, ',')](#join).

*Example 4*

Say you have an object **{ user: {name: 'jack', age: 20} }**. The following function takes that object:

```
indicesAndValues(user)
```

And returns a new object:
```
[
  {
    index: 'name',
    value: 'jack'
  },
  {
    index: 'age',
    value: 20
  }
]
```

<a name="int"></a>

### int

Return the integer version of a string. An exception will be thrown if the string cannot be converted to an integer.

```
int('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string | The string to convert |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*integer-result*> | integer | The integer version of the specified string |
||||

*Example*

This example creates an integer version for the string **10**:

```
int('10')
```

And returns the result as the integer **10**.

<a name="intersection"></a>

### intersection

Return a collection that has only the common items across the specified collections. To appear in the result, an item must appear in all the collections passed to this function. If one or more items have the same name, the last item with that name appears in the result.

```
intersection([<collection1>], [<collection2>], ...)
intersection('<collection1>', '<collection2>', ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection1*>, <*collection2*>  | Yes | array or object, but not both | The collections from which you want only the common items |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*common-items*> | array or object, respectively | A collection that has only the common items across the specified collections |
||||

*Example*

This example finds the common items across the following arrays:

```
intersection(createArray(1, 2, 3), createArray(101, 2, 1, 10), createArray(6, 8, 1, 2))
```

And returns an array with only the items **[1, 2]**.

<a name="isArray"></a>

### isArray

Return `true` if a given input is an array.

```
isArray('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | any | The input to be tested |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*Boolean-result*> | Boolean | Return `true` if a given input is an array, or return `false` if it is not an array. |
||||

*Examples*

The following examples check if the input is an array:

```
isArray('hello')
isArray(createArray('hello', 'world'))
```

And return the following results respectively:

- The input is a string, so the function returns `false`.
- The input is an array, so the function returns `true`.

<a name="isBoolean"></a>

### isBoolean

Return `true` if a given input is a Boolean.

```
isBoolean('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | any | The input to be tested |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*Boolean-result*> | Boolean | Return `true` if a given input is a Boolean, or return `false` if it not a Boolean. |
||||

*Examples*

The following examples check if the input is a Boolean:

```
isBoolean('hello')
isBoolean(32 > 16)
```

And return the following results respectively:

- The input is a string, so the function returns `false`.
- The input is a Boolean, so the function returns `true`.
<a name="isDate"></a>

### isDate

Return `true` if a given TimexProperty or Timex expression refers to a valid date. Valid dates contain the month and dayOfMonth, or contain the dayOfWeek.

```
isDate('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | object or string | The input TimexProperty object or a Timex expression string. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*boolean-result*> | Boolean | Return `true` if the input refers to a valid date, or return `false` if the date is invalid. |
||||

*Examples*

These examples check if the following inputs are valid dates:

```
isDate('2020-12')
isDate('xxxx-12-21')
```

And return the following results:
- `false`
- `true`

<a name="isDateRange"></a>

### isDateRange

Return `true` if a given TimexProperty or Timex expression refers to a valid date range.

```
isDateRange('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | object or string | The input TimexProperty object a Timex expression string. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*boolean-result*> | Boolean | Return `true` if given input refers to a valid date range, or return `false` if it is not a valid date range. |
||||

*Examples*

These examples check if the following input is a valid date range:

```
isDateRange('PT30M')
isDateRange('2012-02')
```

And return the following results:

- `false`
- `true`

<a name="isDateTime"></a>

### isDateTime

Return `true` if a given input is a UTC ISO format (**YYYY-MM-DDTHH:mm:ss.fffZ**) timestamp string.

```
isDateTime('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | any | The input to be tested |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*Boolean-result*> | Boolean | Return `true` if a given input is a UTC ISO format timestamp string, or return `false` if it is not a UTC ISO format timestamp string. |
||||

*Examples*

The following examples check if the input is a UTC ISO format string:

```
isDateTime('hello world!')
isDateTime('2019-03-01T00:00:00.000Z')
```

And return the following results respectively:

- The input is a string, so the function returns `false`.
- The input is a UTC ISO format string, so the function returns `true`.

<a name="isDefinite"></a>

### isDefinite

Return `true` if a given TimexProperty or Timex expression refers to a valid date. Valid dates contain the year, month and dayOfMonth.

```
isDefinite('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | object or string | The input TimexProperty object a Timex expression string. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*boolean-result*> | Boolean | Return `true` if the given input refers to a valid full date, or return `false` if it does not refer to a valid full date. |
||||

*Examples*

Suppose there is a TimexProperty object **validFullDate = new TimexProperty("2020-02-20")** and the `Now` property is set to `true`. The following examples check if the object refers a valid full date:

```
isDefinite('xxxx-12-21')
isDefinite(validFullDate)
```

And return the following results respectively:

- `false`
- `true`

<a name="isDuration"></a>

### isDuration

Return `true` if a given TimexProperty or Timex expression refers to a valid duration.

```
isDuration('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | object or string | The input TimexProperty object a Timex expression string. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*boolean-result*> | Boolean | Return `true` if the input refers to a valid duration, or return `false` if the input doesn't refer to a valid duration. |
||||

*Examples*

The examples below check if the following input refers to a valid duration:

```
isDuration('PT30M')
isDuration('2012-02')
```

And return the following results respectively:

- `true`
- `false`

<a name="isFloat"></a>

### isFloat

Return `true` if a given input is a floating-point number. Due to the alignment between C#and JavaScript, a number with an non-zero residue of its modulo 1 will be treated as a floating-point number.

```
isFloat('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | any | The input to be tested |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*Boolean-result*> | Boolean | Return `true` if a given input is a floating-point number, or return `false` if the input is not a floating-point number. |
||||

*Examples*

The following examples check if the input is a floating-point number:

```
isFloat('hello world!')
isFloat(1.0)
isFloat(12.01)
```

And return the following results respectively:

- The input is a string, so the function returns `false`.
- The input has a modulo that equals 0, so the function returns `false`.
- The input is a floating-point number, so the function returns `true`.

<a name="isInteger"></a>

### isInteger

Return `true` if a given input is an integer number. Due to the alignment between C# and JavaScript, a number with an zero residue of its modulo 1 will be treated as an integer number.

```
isInteger('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | any | The input to be tested |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*Boolean-result*> | Boolean | Is the input is an integer number |
||||

*Examples*

The following examples check if the input is an integer:

```
isInteger('hello world!')
isInteger(1.0)
isInteger(12)
```

And return the following results respectively:

- The input is a string, so the function returns `false`.
- The input has a modulo that equals 0, so the function returns `true`.
- The input is an integer, so the function returns `true`.

<a name="isMatch"></a>

### isMatch

Return `true` if a given string is matches a specified regular expression pattern.

```
isMatch('<target**string>', '<pattern>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*target**string*> | Yes | string | The string to be matched |
| <*pattern*> | Yes | string | A regular expression pattern |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*Boolean-result*> | Boolean | Return `true` if a given string is matches a common regular expression pattern, or return `false` if it does not match the pattern. |
||||

*Examples*

The following examples check if the input matches the specified regular expression pattern:

```
isMatch('ab', '^[a-z]{1,2}$')
isMatch('FUTURE', '(?i)fortune|future')
isMatch('12abc34', '([0-9]+)([a-z]+)([0-9]+)')
isMatch('abacaxc', 'ab.*?c')
```

And return the same result `true`.

<a name="isObject"></a>

### isObject

Return `true` if a given input is a complex object or return `false` if it is a primitive object. Primitive objects include strings, numbers, and Booleans; complex types, like classes, contain properties. <!--In C#, the input is neither a value type nor a string. In JavaScript, it reflects to the input is not a primitive data types.-->

```
isObject('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | any | The input to be tested |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*Boolean-result*> | Boolean | Return `true` if a given input is a complex object, or return `false` if it is a primitive object. |
||||

*Examples*

The following examples check if the given input is an object:

```
isObject('hello world!')
isObject({userName: "Sam"})
```

And return the following results respectively:

- The input is a string, so the function returns `false`.
- The input is an object, so the function returns `true`.

<a name="isPresent"></a>

### isPresent

Return `true` if a given TimexProperty or Timex expression refers to the present.

```
isPresent('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | object or string | The input TimexProperty object a Timex expression string |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*boolean-result*> | Boolean | Return `true` if the input refers to the present, or return false if it doesn't refer to the present. |
||||

*Examples*
Suppose we have an TimexProperty object **validNow = new TimexProperty() { Now = true }** and set the `Now` property to `true`. The examples below check if the following input refers to the present:

```
isPresent('PT30M')
isPresent(validNow)
```

And return the following results respectively:

- `false`
- `true`

<a name="isString"></a>

### isString

Return `true` if a given input is a string.

```
isString('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | any | The input to be tested |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*Boolean-result*> | Boolean | Return `true` if a given input is a string, or return `false` if not a string. |
||||

*Examples*

The following examples check if the given input is a string:

```
isString('hello world!')
isString(3.14)
```

And return the following results respectively:

- The input is a string, so the function returns `true`.
- The input is a float, so the function returns `false`.

<a name="isTime"></a>

### isTime

Return `true` if a given TimexProperty or Timex expression refers to a valid time. Valid time contains hours, minutes and seconds.

```
isTime('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | object or string | The input TimexProperty object a Timex expression string |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*boolean-result*> | Boolean | Return `true` if the input refers to a valid time, or return `false` if it doesn't refer to a valid time.. |
||||

*Examples*

These examples check if the following input refers to a valid time:

```
isTime('PT30M')
isTime('2012-02-21T12:30:45')
```

And return the following results respectively:

- `false`
- `true`

<a name="isTimeRange"></a>

### isTimeRange

Return `true` if a given TimexProperty or Timex expression refers to a valid time range Valid time ranges contain partOfDay.

```
isTime('<input>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*input*> | Yes | object or string | The input TimexProperty object a Timex expression string. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*boolean-result*> | Boolean | Return `true` if  the input refers to a valid time range, or return `false` if it doesn't refer to a valid time range. |
||||

*Examples*

Suppose we have an TimexProperty object **validTimeRange = new TimexProperty() { PartOfDay = "morning" }** and set the `Now` property to `true`. These examples check if the following inputs are valid time ranges:

```
isTimeRange('PT30M')
isTimeRange(validTimeRange)
```

And return the following results respectively:

- `false`
- `true`

<a name="join"></a>

### join

Return a string that has all the items from an array, with each character separated by a *delimiter*.

```
join([<collection>], '<delimiter>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | array | The array that has the items to join |
| <*delimiter*> | Yes | string | The separator that appears between each character in the resulting string |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*char1*><*delimiter*><*char2*><*delimiter*>... | string | The resulting string created from all the items in the specified array |
||||

*Example*

This example creates a string from all the items in this array with the specified character **.** as the delimiter:

```
join(createArray('a', 'b', 'c'), '.')
```

And returns the result **a.b.c**.


<a name="jPath"></a>

### jPath

Check JSON or a JSON string for nodes or values that match a path expression, and return the matching nodes.

```
jPath(<json>, '<path>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*json*> | Yes | any | The JSON object or string to search for nodes or values that match the path expression value |
| <*path*> | Yes | any | The path expression used to find matching JSON nodes or values |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
|[ <*json-node*>] | array | A list of JSON nodes or values that matches the specified path expression |
||||

*C# Example*

Say you have the following JSON:

```json
{
    "Stores": [
        "Lambton Quay",
        "Willis Street"
    ],
    "Manufacturers": [
        {
            "Name": "Acme Co",
            "Products": [
                {
                    "Name": "Anvil",
                    "Price": 50
                }
            ]
        },
        {
            "Name": "Contoso",
            "Products": [
                {
                    "Name": "Elbow Grease",
                    "Price": 99.95
                },
                {
                    "Name": "Headlight Fluid",
                    "Price": 4
                }
            ]
        }
    ]
}
```

The path expression is **$..Products[?(@.Price >= 50)].Name**

```
jPath(jsonStr, path)
```

And it returns the result **["Anvil", "Elbow Grease"]**.

*JavaScript Example*

Say you have the following JSON:

```json
{
    "automobiles": [
        {
            "maker": "Nissan",
            "model": "Teana",
            "year": 2011
        },
        {
            "maker": "Honda",
            "model": "Jazz",
            "year": 2010
        },
        {
            "maker": "Honda",
            "model": "Civic",
            "year": 2007
        },
        {
            "maker": "Toyota",
            "model": "Yaris",
            "year": 2008
        },
        {
            "maker": "Honda",
            "model": "Accord",
            "year": 2011
        }
    ],
    "motorcycles": [
        {
            "maker": "Honda",
            "model": "ST1300",
            "year": 2012
        }
    ]
}
```

The path expression is **.automobiles{.maker === "Honda" && .year > 2009}.model**.

```
jPath(jsonStr, path)
```

And it returns the result **['Jazz', 'Accord']**.

<a name="json"></a>

### json

Return the JavaScript Object Notation (JSON) type value or object of a string or XML.

```
json('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string or XML | The string or XML to convert |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*JSON-result*> | string | The resulting JSON object created from the specified string or XML. |
||||

*Example 1*

This example converts a string to JSON:

```
json('{"fullName": "Sophia Owen"}')
```

And returns the result:

```
{
  "fullName": "Sophia Owen"
}
```

*Example 2*

This example converts XML to JSON:

```
json(xml('<?xml version="1.0"?> <root> <person id='1'> <name>Sophia Owen</name> <occupation>Engineer</occupation> </person> </root>'))
```

And returns the result:

```
{
   "?xml": { "@version": "1.0" },
   "root": {
      "person": [ {
         "@id": "1",
         "name": "Sophia Owen",
         "occupation": "Engineer"
      } ]
   }
}
```

<a name="jsonStringify"></a>

### jsonStringify

Return the JSON string of a value.

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | object | The object to convert to a JSON string |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*JSON-result*> | string | The resulting JSON string. |
||||

*Examples*

These examples show objects converted to JSON strings:

```
jsonStringify(null)
jsonStringify({a:'b'})
```

And return the following string results respectively:

-**null**
-**{\"a\":\"b\"}**

<a name="last"></a>

### last

Return the last item from a collection.

```
last('<collection>')
last([<collection>])
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | string or array | The collection in which to find the last item |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*last-collection-item*> | string or array, respectively | The last item in the collection |
||||

*Example*

These examples find the last item in these collections:

```
last('abcd')
last(createArray(0, 1, 2, 3))
```

And returns the following results respectively:

* **d**
* **3**

<a name="lastIndexOf"></a>

### lastIndexOf

Return the starting position or index value of the last occurrence of a substring. This function is case-insensitive, and indexes start with the number 0.

```
lastIndexOf('<text>', '<searchText>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string or array | The string that has the substring to find |
| <*searchText*> | Yes | string | The substring to find |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*ending-index-value*> | integer | The starting position or index value of the last occurrence of the specified substring. If the string is not found, return the number **-1**. |
||||

*Example 1*

This example finds the starting index value of the last occurrence of the substring **world** in the **hello world** string:

```
lastIndexOf('hello world', 'world')
```

And returns the result **6**.

*Example 2*

This example finds the starting index value of the last occurrence of substring **def** in the array **['abc', 'def', 'ghi', 'def']**.

```
lastIndexOf(createArray('abc', 'def', 'ghi', 'def'), 'def')
```

And returns the result **3**.

### length

Return the length of a string.

```
length('<str>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*str*> | Yes | string | The string to calculate for length |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*length*> | integer | The length of this string |
||||

*Examples*

These examples get the length of strings:

```
length('hello')
length('hello world')
```

And returns the following results respectively:

* **5**
* **11**

<a name="less"></a>

### less

Check whether the first value is less than the second value. Return `true` if the first value is less, or return `false` if the first value is more.

```
less(<value>, <compareTo>)
less('<value>', '<compareTo>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | integer, float, or string | The first value to check whether less than the second value |
| <*compareTo*> | Yes | integer, float, or string, respectively  | The comparison item |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` if the first value is less than the second value. Return `false` if the first value is equal to or greater than the second value. |
||||

*Examples*

These examples check whether the first value is less than the second value.

```
less(5, 10)
less('banana', 'apple')
```

And return the following results respectively:

* `true`
* `false`

<a name="lessOrEquals"></a>

### lessOrEquals

Check whether the first value is less than or equal to the second value. Return `true` if the first value is less than or equal,
or return `false` if the first value is more.

```
lessOrEquals(<value>, <compareTo>)
lessOrEquals('<value>', '<compareTo>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | integer, float, or string | The first value to check whether less than or equal to the second value |
| <*compareTo*> | Yes | integer, float, or string, respectively  | The comparison item |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false  | Boolean | Return `true` if the first value is less than or equal to the second value. Return `false` if the first value is greater than the second value. |
||||

*Example*

These examples check whether the first value is less than or equal to the second value.

```
lessOrEquals(10, 10)
lessOrEquals('apply', 'apple')
```

And return the following results respectively:

* `true`
* `false`

<a name="max"></a>

### max

Return the highest value from a list or array. The list or array is inclusive at both ends.

```
max(<number1>, <number2>, ...)
max([<number1>, <number2>, ...])
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*number1*>, <*number2*>, ... | Yes | number | The set of numbers from which you want the highest value |
| [<*number1*>, <*number2*>, ...] | Yes | array of numbers | The array of numbers from which you want the highest value |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*max-value*> | number | The highest value in the specified array or set of numbers |
||||

*Examples*

These examples get the highest value from the set of numbers and the array:

```
max(1, 2, 3)
max(createArray(1, 2, 3))
```

And return the result **3**.

<a name="merge"></a>

### merge

Merges multiple JSON objects or an array of objects together.

```
merge(<json1>, <json2>, ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*json1*>, <*json2*>, ... | Yes | objects or array | The set of JSON objects or array to merge together. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*result*> | object | The combined JSON object or combined array objects. |
||||

*Examples*

Say you have the following JSON objects:

```json
json1 = @"{
            'FirstName': 'John',
            'LastName': 'Smith',
            'Enabled': false,
            'Roles': [ 'User' ]
          }"
json2 =@"{
            'Enabled': true,
            'Roles': [ 'User', 'Admin' ]
          }"
```

This example merges the JSON objects:

```
string(merge(json(json1), json(json2)))
```
And returns the resulting object **{"FirstName":"John","LastName":"Smith","Enabled":true,"Roles":["User","Admin"]}**.

Say you want to combine objects and a list of objects together. The following example combines JSON object and an array of objects:

```
merge({k1:'v1'}, [{k2:'v2'}, {k3: 'v3'}], {k4:'v4'})
```

And returns the object **{ ​"k1":"v1", ​"k2":"v2", ​"k3":"v3", "k4":"v4"}**.

<a name="min"></a>

### min

Return the lowest value from a set of numbers or an array.

```
min(<number1>, <number2>, ...)
min([<number1>, <number2>, ...])
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*number1*>, <*number2*>, ... | Yes | number | The set of numbers from which you want the lowest value |
| [<*number1*>, <*number2*>, ...] | Yes | array of numbers | The array of numbers from which you want the lowest value |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*min-value*> | number | The lowest value in the specified array or set of numbers |
||||

*Examples*

These examples get the lowest value in the set of numbers and the array:

```
min(1, 2, 3)
min(createArray(1, 2, 3))
```

And return the result **1**.

<a name="mod"></a>

### mod

Return the remainder from dividing two numbers. To get the integer result, see [div()](#div).

```
mod(<dividend>, <divisor>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*dividend*> | Yes | number | The number to divide by the *divisor* |
| <*divisor*> | Yes | number | The number that divides the *dividend*. Cannot be 0. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*modulo-result*> | number | The remainder from dividing the first number by the second number |
||||

*Example*

This example divides the first number by the second number:

```
mod(3, 2)
```

And returns the result **1**.

<a name="month"></a>

### month

Return the month of the specified timestamp.

```
month('<timestamp>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*number-of-month*> | integer | The number of the month in the specified timestamp |
||||

*Example*

```
month('2018-03-15T13:01:00.000Z')
```

And it returns the result **3**.

<a name="mul"></a>

### mul

Return the product from multiplying two numbers.

```
mul(<multiplicand1>, <multiplicand2>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*multiplicand1*> | Yes | integer or float | The number to multiply by *multiplicand2* |
| <*multiplicand2*> | Yes | integer or float | The number that multiples *multiplicand1* |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*product-result*> | integer or float | The product from multiplying the first number by the second number |
||||

*Examples*

These examples multiple the first number by the second number:

```
mul(1, 2)
mul(1.5, 2)
```

And return the following results respectively:

* **2**
* **3**

<a name="newGuid"></a>

### newGuid

Return a new Guid string.

```
newGuid()
```

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*Guid-string*> | string | A new Guid string, length is 36 and looks like *xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx*|
||||

*Example*

```
newGuid()
```

And it returns a result which follows the format **xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx**.

<a name="not"></a>

### not

Check whether an expression is false. Return `true` if the expression is false, or return `false` if true.

```
not(<expression>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*expression*> | Yes | Boolean | The expression to check |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` if the expression is false. Return `false` if the expression is true. |
||||

*Example 1*

These examples check whether the specified expressions are false:

```
not(false)
not(true)
```

And return the following results respectively:

* The expression is false, so the function returns `true`.
* The expression is true, so the function returns `false`.

*Example 2*

These examples check whether the specified expressions are false:

```
not(equals(1, 2))
not(equals(1, 1))
```

And return the following results respectively:

* The expression is false, so the function returns `true`.
* The expression is true, so the function returns `false`.

<a name="or"></a>

### or

Check whether at least one expression is true. Return `true` if at least one expression is true,
or return `false` if all are false.

```
or(<expression1>, <expression2>, ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*expression1*>, <*expression2*>, ... | Yes | Boolean | The expressions to check |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` if at least one expression is true. Return `false` if all expressions are false. |
||||

*Example 1*

These examples check whether at least one expression is true:

```
or(true, false)
or(false, false)
```

And return the following results respectively:

* At least one expression is true, so the function returns `true`.
* Both expressions are false, so the function returns `false`.

*Example 2*

These examples check whether at least one expression is true:

```
or(equals(1, 1), equals(1, 2))
or(equals(1, 2), equals(1, 3))
```

And return the following results respectively:

* At least one expression is true, so the function returns `true`.
* Both expressions are false, so the function returns `false`.

<a name="rand"></a>

### rand

Return a random integer from a specified range, which is inclusive only at the starting end.

```
rand(<minValue>, <maxValue>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*minValue*> | Yes | integer | The lowest integer in the range |
| <*maxValue*> | Yes | integer | The integer that follows the highest integer in the range that the function can return |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*random-result*> | integer | The random integer returned from the specified range |
||||

*Example*

This example gets a random integer from the specified range, excluding the maximum value:

```
rand(1, 5)
```

And returns **1**, **2**, **3**, or **4** as the result.

<a name="range"></a>

### range

Return an integer array that starts from a specified integer.

```
range(<startIndex>, <count>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*startIndex*> | Yes | integer | An integer value that starts the array as the first item |
| <*count*> | Yes | integer | The number of integers in the array |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*range-result*> | integer | The array with integers starting from the specified index |
||||

*Example*

This example creates an integer array that starts from the specified index **1** and has the specified number of integers as **4**:

```
range(1, 4)
```

And returns the result **[1, 2, 3, 4]**.

<a name="removeProperty"></a>

### removeProperty

Remove a property from an object and return the updated object.

```
removeProperty(<object>, '<property>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*object*> | Yes | object | The JSON object in which you want to remove a property |
| <*property*> | Yes | string | The name of the property to remove |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-object*> | object | The updated JSON object without the specified property |
||||

*Example*

This example removes the `accountLocation` property from a `customerProfile` object, which is converted to JSON with the [json()](#json) function, and returns the updated object:

```
removeProperty(json('customerProfile'), 'accountLocation')
```

<a name="replace"></a>

### replace

Replace a substring with the specified string, and return the result string. This function is case-sensitive.

```
replace('<text>', '<oldText>', '<newText>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string that has the substring to replace |
| <*oldText*> | Yes | string | The substring to replace |
| <*newText*> | Yes | string | The replacement string |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-text*> | string | The updated string after replacing the substring. If the substring is not found, the function returns the original string. |
||||

*Example 1*

This example finds the substring **old** in **the old string** and replaces **old** with **new**:

```
replace('the old string', 'old', 'new')
```

The result is the string **the new string**.

*Example 2*

When dealing with escape characters, the expression engine handles the unescape for you. This function replaces strings with escape characters.

```
replace('hello\"', '\"', '\n')
replace('hello\n', '\n', '\\\\')
@"replace('hello\\', '\\', '\\\\')"
@"replace('hello\n', '\n', '\\\\')"
```

And returns the following results respectively:

- **hello\n**
- **hello\\\\**
- **@"hello\\\\"**
- **@"hello\\\\"**

<a name="replaceIgnoreCase"></a>

### replaceIgnoreCase

Replace a substring with the specified string, and return the result string. This function is case-insensitive.

```
replaceIgnoreCase('<text>', '<oldText>', '<newText>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string that has the substring to replace |
| <*oldText*> | Yes | string | The substring to replace |
| <*newText*> | Yes | string | The replacement string |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-text*> | string | The updated string after replacing the substring. If the substring is not found, return the original string. |
||||

*Example*

This example finds the substring **old** in the string **the old string** and replaces **old** with **new**:

```
replace('the old string', 'old', 'new')
```

And returns the result **the new string**.

<a name="resolve"></a>

### resolve

Return string of a given TimexProperty or Timex expression if it refers to a valid time. Valid time contains hours, minutes, and seconds.

```
resolve('<timestamp')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*return*> | string| A string of the evaluated TimexProperty or Timex expression. |
||||


*Examples*

These examples show if the given strings refer to valid time:

```
resolve(T14)
resolve(2020-12-20)
resolve(2020-12-20T14:20)
```

And returns the following results respectively:
-**14:00:00**
-**2020-12-20**
-**2020-12-20 14:20:00**


<a name="reverse"></a>

### reverse

Reverse the order of the elements in a string or array.

```
reverse(<value>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string or array | The string to array to reverse. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*return*> | string or array | The reversed elements of a string or array. |
||||

*Examples*

These examples reverse the elements of a string or array:

```
reverse(hello)
reverse(concat(hello,world))
```

And return the following values respectively:
-The string **olleh**.
-The string **dlrowolleh**.


<a name="round"></a>

### round

Round a value to the nearest integer or to the specified number of fractional digits.

```
round('<number>', '<precision-digits>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*number*> | Yes | number | An input number |
| <*precision-digits*> | No | integer | A specified number of fractional digits. The default is 0. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*return-value*> | number | The return value of the input rounded at a specified number of fractional digits |
||||

*Example 1*

This example rounds the number **10.333**:

```
round(10.333)
```

And returns the number **10**.

*Example 2*

This example rounds the number **10.3313** to **2** fractional digits:

```
round(10.3313, 2)
```

And returns the number **10.33**.


<a name="select"></a>

### select

Operate on each element and return the new collection of transformed elements.

```
select([<collection/instance>], <iteratorName>, <function>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection/instance*> | Yes | array | The collection with the items |
| <*iteratorName*> | Yes | iterator name | The key item |
| <*function*> | Yes | expression | Th function that can contains `iteratorName` |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*new-collection*> | array | The new collection in which each element has been evaluated with the function  |
||||

*Example 1*

This example generates a new collection:

```
select(createArray(0, 1, 2, 3), x, x + 1)
```

And returns the result **[1, 2, 3, 4]**.

*Example 2*

These examples generate a new collection:

```
select(json("{'name': 'jack', 'age': '15'}"), x, concat(x.key, ':', x.value))
select(json("{'name': 'jack', 'age': '15'}"), x=> concat(x.key, ':', x.value))

```

And return the result **['name:jack', 'age:15']**. Note that the second expression is a *lambda expression*, which some find more readable.

<a name="sentenceCase"></a>

### sentenceCase

Capitalize the first letter of the first word in a string in an optional locale format.

```
sentenceCase('<text>', '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The original string |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| result string | string | Returns the sentence case result |
||||

*Example 1*

These examples capitalize the first letter in a string:

```
sentenceCase('a')
sentenceCase('abc def')
sentenceCase('aBC dEF')
```

And return the following results respectively:

- **A**
- **Abc def**
- **Abc def**

*Example 2*

These examples capitalizes the first letter in a string in the specified locale format:

```
sentenceCase('a', 'fr-FR')
sentenceCase('abc', 'en-US')
sentenceCase('aBC', 'fr-FR')
```

And return the following results respectively:

- **A**
- **Abc**
- **Abc**

<a name="setPathToValue"></a>

### setPathToValue

Retrieve the value of the specified property from the JSON object.

```
setPathToValue(<path>, <value>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*Path*> | Yes | object | The path which you want to set |
| <*value*> | Yes | object | The value you want to set to the path |

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| value | object | The value be set |
||||

*Example 1*

The example below sets the value **1** to the path:

```
setPathToValue(path.x, 1)
```

And returns the result **1**. `path.x` is set to **1**.

*Example 2*

This example below sets the value:

```
setPathToValue(path.array[0], 7) + path.array[0]
```

And returns the result **14**.

<a name="setProperty"></a>

### setProperty

Set the value of an object's property and return the updated object. To add a new property, use this function or the [addProperty()](#addProperty) function.

```
setProperty(<object>, '<property>', <value>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*object*> | Yes | object | The JSON object in which you want to set a property |
| <*property*> | Yes | string | The name of the property to set |
| <*value*> | Yes | any | The value to set for the specified property |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-object*> | object | The updated JSON object whose property you set |
||||

*Example*

This example sets the `accountNumber` property on a `customerProfile` object, which is converted to JSON with the [json()](#json) function. The function assigns a value generated by the [newGuid()](#newGuid) function, and returns the updated JSON object:

```
setProperty(json('customerProfile'), 'accountNumber', newGuid())
```

<a name="skip"></a>

### skip

Remove items from the front of a collection, and return all the other items.

```
skip([<collection>], <count>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | array | The collection whose items you want to remove |
| <*count*> | Yes | integer | A positive integer for the number of items to remove at the front |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updatedCollection*> | array | The updated collection after removing the specified items |
||||

*Example*

This example removes one item, the number *1*, from the front of the specified array:

```
skip(createArray(0, 1, 2, 3), 1)
```

And returns an array with the remaining items: **[1,2,3]**.

<a name="sortBy"></a>

### sortBy

Sort elements in the collection in ascending order and return the sorted collection.

```
sortBy([<collection>], '<property>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | string or array | The collection to sort |
| <*property*> | No | string | Sort by this specific property of the object element in the collection if set|
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*new-collection*> | array | The new collection whose elements have been sorted |
||||

*Example 1*

This example generates sorts the following collection:

```
sortBy(createArray(1, 2, 0, 3))
```

And return the result **[0, 1, 2, 3]**.

*Example 2*

Suppose you have the following collection:

```
{
  'nestedItems': [
    {'x': 2},
    {'x': 1},
    {'x': 3}
  ]
}
```

This example generates a new sorted collection based on the **x** object property

```
sortBy(nestedItems, 'x')
```

And returns the result:

```
{
  'nestedItems': [
    {'x': 1},
    {'x': 2},
    {'x': 3}
  ]
}
```

<a name="sortByDescending"></a>

### sortByDescending

Sort elements in the collection in descending order, and return the sorted collection.

```
sortBy([<collection>], '<property>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | string or array | The collection to sort |
| <*property*> | No | string | Sort by this specific property of the object element in the collection if set|
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*new-collection*> | array | The new collection whose elements have been sorted |
||||

*Example 1*

This example generates a new sorted collection:

```
sortByDescending(createArray(1, 2, 0, 3))
```

And returns the result **[3, 2, 1, 0]**.

*Example 2*

Suppose you have the following collection:

```
{
  'nestedItems': [
    {'x': 2},
    {'x': 1},
    {'x': 3}
  ]
}
```

This example generates a new sorted collection based on the **x** object property:

```
sortByDescending(nestedItems, 'x')
```

And returns this result:

```
{
  'nestedItems': [
    {'x': 3},
    {'x': 2},
    {'x': 1}
  ]
}
```

<a name="split"></a>

### split

Return an array that contains substrings, separated by commas, based on the specified delimiter character in the original string.

```
split('<text>', '<delimiter>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string to separate into substrings based on the specified delimiter in the original string. If the text is a null value, it will be taken as an empty string. |
| <*delimiter*> | No | string | The character in the original string to use as the delimiter. If no delimiter provided or the delimiter is a null value, the default value will be an empty string. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| [<*substring1*>,<*substring2*>,...] | array | An array that contains substrings from the original string, separated by commas |
||||

*Examples*

These examples create an array with substrings from the specified string based on the specified delimiter character:

```
split('a**b**c', '**')
split('hello', '')
split('', 'e')
split('', '')
split('hello')
```

And returns the following arrays as the result respectively:
- **["a", "b", "c"]**
- **["h", "e", "l", "l", "o"]**
- **[""]**, **[ ]**
- **["h", "e", "l", "l", "o"]**

<a name="sqrt"></a>

### sqrt

Return the square root of a specified number.

```
sqrt(<number>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*number*> | Yes | number | Number to get square root of of |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*result*> | number| The result from computing the square root.|
||||

*Examples*

These examples compute the square root of specified numbers:

```
sqrt(9)
sqrt(0)
```

And return the following results respectively:
-**3**
-**0**

<a name="startOfDay"></a>

### startOfDay

Return the start of the day for a timestamp in an optional locale format.

```
startOfDay('<timestamp>', '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| '<*updated-timestamp*>'| string | The specified timestamp starting at the zero-hour mark for the day |
||||

*Example 1*

This example finds the start of the day:

```
startOfDay('2018-03-15T13:30:30.000Z')
```

And returns the result **2018-03-15T00:00:00.000Z**.

*Example 2*

This example finds the start of the day with the locale **fr-FR**:

```
startOfDay('2018-03-15T13:30:30.000Z', '', 'fr-FR')
```

And returns the result **15/03/2018 00:00:00**.

<a name="startOfHour"></a>

### startOfHour

Return the start of the hour for a timestamp in an optional locale format.

```
startOfHour('<timestamp>', '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| '<*updated-timestamp*>'| string | The specified timestamp starting at the zero-minute mark for the day |
||||

*Example 1*

This example finds the start of the hour:

```
startOfHour('2018-03-15T13:30:30.000Z')
```

And returns the result **2018-03-15T13:00:00.000Z**.

*Example 2*

This example finds the start of the hour with the locale **fr-FR**:

```
startOfHour('2018-03-15T13:30:30.000Z', '', 'fr-FR')
```

And returns the result **15/03/2018 13:00:00**.

<a name="startOfMonth"></a>

### startOfMonth

Return the start of the month for a timestamp in an optional locale format.

```
startOfMonth('<timestamp>', '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| '<*updated-timestamp*>'| string | The specified timestamp starting on the first day of the month at the zero-hour mark |
||||

*Example 1*

This example finds the start of the month:

```
startOfMonth('2018-03-15T13:30:30.000Z')
```

And returns the result **2018-03-01T00:00:00.000Z**.

*Example 2*

This example finds the start of the month with the locale **fr-FR**:

```
startOfMonth('2018-03-15T13:30:30.000Z', '', 'fr-FR')
```

And returns the result **01/03/2018 00:00:00**.

<a name="startsWith"></a>

### startsWith

Check whether a string starts with a specific substring. Return `true` if the substring is found, or return `false` if not found. This function is case-insensitive.

```
startsWith('<text>', '<searchText>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string to check |
| <*searchText*> | Yes | string | The starting substring to find |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| true or false | Boolean | Return `true` if the starting substring is found. Return `false` if not found |
||||

*Example 1*

This example checks whether the string **hello world** starts with the string **hello**:

```
startsWith('hello world', 'hello')
```

And returns the result `true`.

*Example 2*

This example checks whether the string **hello world** starts with the string **greeting**:

```
startsWith('hello world', 'greeting')
```

And returns the result `false`.

<a name="string"></a>

### string

Return the string version of a value in an optional locale format.

```
string(<value>, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | any | The value to convert |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*string-value*> | string | The string version of the specified value |
||||

*Example 1*

This example creates the string version of the number **10**:

```
string(10)
```

And returns the string result **10**.

*Example 2*

This example creates a string for the specified JSON object and uses the backslash character,**\\\\**, as an escape character for the double-quotation mark character, **"**.

```
string( { "name": "Sophie Owen" } )
```

And returns the result **{ "name": "Sophie Owen" }**

*Example 3*

These example creates a string version of the number **10** in a specific locale:

```
string(100.1, 'fr-FR')
string(100.1, 'en-US')
```

And returns the following strings respectively:

- **100,1**
- **100.1*

<a name="stringOrValue"></a>

### stringOrValue

Wrap string interpolation to get the real value. For example, stringOrValue('${1}') returns the number 1, while stringOrValue('${1} item') returns the string "1 item".

```
stringOrValue(<string>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*string*> | Yes | string | The string to get the real value from. |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*result*> | any | The result of getting the real value of the specified string. |
||||

*Examples*

These examples get the real value from the string:

```
stringOrValue('${one}')
stringOrValue('${one} item')
```

And return the following results respectively:
- The number **1.0**.
- The string **1 item**.

<a name="sub"></a>

### sub

Return the result from subtracting the second number from the first number.

```
sub(<minuend>, <subtrahend>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*minuend*> | Yes | number | The number from which to subtract the *subtrahend* |
| <*subtrahend*> | Yes | number | The number to subtract from the *minuend* |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*result*> | number | The result from subtracting the second number from the first number |
||||

*Example*

This example subtracts the second number from the first number:

```
sub(10.3, .3)
```

And returns the result **10**.

<a name="subArray"></a>

### subArray

Returns a subarray from specified start and end positions. Index values start with the number 0.

```
subArray(<Array>, <startIndex>, <endIndex>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*array*> | Yes | array | The array to create the subarray from |
| <*startIndex*> | Yes | integer | A positive number equal to or greater than 0 to use as the starting position or index value |
| <*endIndex*> | Yes | integer |  A positive number equal to or greater than 0 to use as the ending position or index value|
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*subarray-result*> | array | A subarray with the specified number of items, starting at the specified index position in the source string |
||||

*Example*

This example creates a subarray from the specified array:

```
subArray(createArray('H','e','l','l','o'), 2, 5)
```

And returns the result **["l", "l", "o"]**.

<a name="substring"></a>

### substring

Return characters from a string, starting from the specified position or index. Index values start with the number 0.

```
substring('<text>', <startIndex>, <length>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string to create the substring from |
| <*startIndex*> | Yes | integer | A positive number equal to or greater than 0 subarray to use as the starting position or index value |
| <*length*> | Yes | integer | A positive number of characters subarray in the substring |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*substring-result*> | string | A substring with the specified number of characters, starting at the specified index position in the source string |
||||

*Example*

This example creates a five-character substring from the specified string, starting from the index value 6:

```
substring('hello world', 6, 5)
```

And returns the result **world**.

<a name="subtractFromTime"></a>

### subtractFromTime

Subtract a number of time units from a timestamp in an optional locale format. See also [getPastTime()](#getPastTime).

```
subtractFromTime('<timestamp>', <interval>, '<timeUnit>', '<format>'?, '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
| <*interval*> | Yes | integer | The number of specified time units to subtract |
| <*timeUnit*> | Yes | string | The unit of time to use with *interval*. Possible units are "Second", "Minute", "Hour", "Day", "Week", "Month", and "Year". |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updated-timestamp*> | string | The timestamp minus the specified number of time units |
||||

*Example 1*

This example subtracts one day from a following timestamp:

```
subtractFromTime('2018-01-02T00:00.000Z', 1, 'Day')
```

And returns the result **2018-01-01T00:00:00.000Z**.

*Example 2*

This example subtracts one day from a timestamp using the **D** format:

```
subtractFromTime('2018-01-02T00:00.000Z', 1, 'Day', 'D')
```

And returns the result **Monday, January, 1, 2018**.

*Example 3*

This example subtracts **1** hour from a timestamp in the **de-DE** locale:

```
subtractFromTime('2018-03-15T13:00:00.000Z', 1, 'Hour', '', 'de-DE')
```

And returns the result **15.03.18 12:00:00**.

<a name="sum"></a>

### sum

Return the result from adding numbers in a list.

```
sum([<list of numbers>])
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| [\<list of numbers\>] | Yes | array of numbers | The numbers to add |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*result-sum*> | number | The result from adding the specified numbers |
||||

*Example*

This example adds the specified numbers:

```
sum(createArray(1, 1.5))
```

And returns the result **2.5**.

<a name="take"></a>

### take

Return items from the front of a collection.

```
take('<collection>', <count>)
take([<collection>], <count>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | string or array | The collection whose items you want |
| <*count*> | Yes | integer | A positive integer for the number of items you want from the front |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*subset*> or [<*subset*>]| String or Array, respectively | A string or array that has the specified number of items taken from the front of the original collection |
||||

*Example*

These examples get the specified number of items from the front of these collections:

```
take('abcde', 3)
take(createArray(0, 1, 2, 3, 4), 3)
```

And return the following results respectively:

- **abc**
- **[0, 1, 2]**

<a name='ticks'></a>

### ticks

Return the ticks property value of a specified timestamp. A tick is 100-nanosecond interval.

```
ticks('<timestamp>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*>| Yes | string | The string for a timestamp |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*ticks-number*> | integer (bigint in JavaScript)| The number of ticks since the specified timestamp |
||||

*Example*

This example converts a timestamp to its ticks property:

```
ticks('2018-01-01T08:00:00.000Z')
```

And returns the result **636503904000000000**.

<a name='ticksToDays'></a>

### ticksToDays

Convert a ticks property value to the number of days.

```
ticksToDays('ticks')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*ticks*>| Yes | integer | The ticks property value to convert |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*number-of-days*> | number | The number of days converted from the ticks property value |
||||

*Example*

This example converts a ticks property value to a number of days:

```
ticksToDays(2193385800000000)
```

And returns the number **2538.64097222**.

<a name='ticksToHours'></a>

### ticksToHours

Convert a ticks property value to the number of hours.

```
ticksToHours('ticks')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*ticks*>| Yes | Integer | The ticks property value to convert |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*number-of-hours*> | number | The number of hours converted from the ticks property value |
||||

*Example*

This example converts a ticks property value to a number of hours:

```
ticksToHours(2193385800000000)
```

And returns the number **60927.383333333331**.

<a name='ticksToMinutes'></a>

### ticksToMinutes

Convert a ticks property value to the number of minutes.

```
ticksToMinutes('ticks')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*ticks*>| Yes | integer | The ticks property value to convert |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*number-of-minutes*> | number | The number of minutes converted from the ticks property value |
||||

*Example*

This example converts a ticks property value to a number of minutes:

```
ticksToMinutes(2193385800000000)
```

And returns the number **3655643.0185**.

<a name="titleCase"></a>

### titleCase

Capitalize the first letter of each word in a string in an optional local format.

```
titleCase('<text>', '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The original string |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| result string | string | The title case result |
||||

*Example 1*

These examples capitalize the first letter of each word in a string:

```
titleCase('a')
titleCase('abc def')
titleCase('aBC dEF')
```

And return the following results respectively:

- **A**
- **Abc Def**
- **Abc Def**

*Example 2*

These examples capitalize the first letter in a string in the **en-US** format:

```
titleCase('a', 'en-US')
titleCase('aBC dEF', 'en-US')
```

And return the following results respectively:

- **A**
- **Abc Def**

<a name="toLower"></a>

### toLower

Return a string in lowercase in an optional locale format. If a character in the string doesn't have a lowercase version, that character stays unchanged in the returned string.

```
toLower('<text>', '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string to return in lowercase format |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*lowercase-text*> | string | The original string in lowercase format |
||||

*Example 1*

This example converts a string to lowercase:

```
toLower('Hello World')
```

And returns the result **hello world**.

*Example 2*

This example converts a string to lowercase in the **fr-FR** format:

```
toUpper('Hello World', 'fr-FR')
```

And returns the result **hello world**.

<a name="toUpper"></a>

### toUpper

Return a string in uppercase in an optional locale format. If a character in the string doesn't have an uppercase version, that character stays unchanged in the returned string.

```
toUpper('<text>', '<locale>'?)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string to return in uppercase format |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*uppercase-text*> | string | The original string in uppercase format |
||||

*Example 1*

This example converts a string to uppercase:

```
toUpper('Hello World')
```

And returns the result **HELLO WORLD**.

*Example 2*

This example converts a string to uppercase in the **fr-FR** format:

```
toUpper('Hello World', 'fr-FR')
```

And returns the result **HELLO WORLD**.

<a name="trim"></a>

### trim

Remove leading and trailing whitespace from a string, and return the updated string.

```
trim('<text>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*text*> | Yes | string | The string that has the leading and trailing whitespace to remove |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updatedText*> | string | An updated version for the original string without leading or trailing whitespace |
||||

*Example*

This example removes the leading and trailing whitespace from the string **" Hello World  "**:

```
trim(' Hello World  ')
```

And returns the trimmed result **Hello World**.

<a name="union"></a>

### union

Return a collection that has all the items from the specified collections. To appear in the result, an item can appear in any collection passed to this function. If one or more items have the same name, the last item with that name appears in the result.

```
union('<collection1>', '<collection2>', ...)
union([<collection1>], [<collection2>], ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection1*>, <*collection2*>, ...| Yes | array or object, but not both | The collections from where you want all the items |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*updatedCollection*> | array or object, respectively | A collection with all the items from the specified collections. No duplicates added. |
||||

*Example*

This example gets all the items from the following collections:

```
union(createArray(1, 2, 3), createArray(1, 2, 10, 101))
```

And returns the result **[1, 2, 3, 10, 101]**.

<a name="unique"/>

### unique

Remove all duplicates from an array.

```
unique([<collection>])
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection*> | Yes | array | The collection to modify |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*new-collection*> | array | New collection with duplicate elements removed |
||||

*Example 1*

This example removes duplicate elements from the following array:

```
unique(createArray(1, 2, 1))
```

And returns the result **[1, 2]**.

<a name="uriComponent"></a>

### uriComponent

Return the binary version of a uniform resource identifier (URI) component.

```
uriComponent('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string | The string to convert to URI-encoded format |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*encoded-uri*> | string | The URI-encoded string with escape characters |
||||

*Example*

This example creates a URI-encoded version of a string:

```
uriComponent('https://contoso.com')
```

And returns the result **http%3A%2F%2Fcontoso.com**.

<a name="uriComponentToString"></a>

### uriComponentToString

Return the string version of a uniform resource identifier (URI) encoded string, effectively decoding the URI-encoded string.

```
uriComponentToString('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string | The URI-encoded string to decode |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*binary-for-encoded-uri*> | string | The decoded version for the URI-encoded string |
||||

*Example*

This example creates the decoded string version of a URI-encoded string:

```
uriComponentToString('http%3A%2F%2Fcontoso.com')
```

And returns the result `https://contoso.com`.

<a name="uriHost"></a>

### uriHost

Return the host value of a unified resource identifier (URI).

```
uriHost('<uri>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*uri*> | Yes | string | The URI whose host value you want |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*host-value*> | string | The host value of the specified URI |
||||

*Example*

This example finds the host value of the following URI:

```
uriHost('https://www.localhost.com:8080')
```

And returns the result `www.localhost.com`.

<a name="uriPath"></a>

### uriPath

Return the path value of a unified resource identifier (URI).

```
uriPath('<uri>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*uri*> | Yes | string | The URI whose path value you want |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*path-value*> | string | The path value of the specified URI |
||||

*Example*

This example finds the path value of the following URI:

```
uriPath('http://www.contoso.com/catalog/shownew.htm?date=today')
```

And returns the result **/catalog/shownew.htm**.

<a name="uriPathAndQuery"></a>

### uriPathAndQuery

Return the path and query value of a unified resource identifier (URI).

```
uriPathAndQuery('<uri>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*uri*> | Yes | string | The URI whose path and query value you want |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*path-query-value*> | string | The path and query value of the specified URI |
||||

*Example*

This example finds the path and query value of the following URI:

```
uriPathAndQuery('http://www.contoso.com/catalog/shownew.htm?date=today')
```

And returns the result **/catalog/shownew.htm?date=today**.

<a name="uriPort"></a>

### uriPort

Return the port value of a unified resource identifier (URI).

```
uriPort('<uri>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*uri*> | Yes | string | The URI whose path value you want |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*port-value*> | string | The port value of the specified URI |
||||

*Example*

This example finds the port value of the following URI:

```
uriPort('http://www.localhost:8080')
```

And returns the result **8080**.

<a name="uriQuery"></a>

### uriQuery

Return the query value of a unified resource identifier (URI).

```
uriQuery('<uri>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*uri*> | Yes | string | The URI whose query value you want |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*query-value*> | string | The query value of the specified URI |
||||

*Example*

This example finds the query value of the following URI:

```
uriQuery('http://www.contoso.com/catalog/shownew.htm?date=today')
```

And returns the result **?date=today**.

<a name="uriScheme"></a>

### uriScheme

Return the scheme value of a unified resource identifier (URI).

```
uriScheme('<uri>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*uri*> | Yes | string | The URI whose query value you want |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*scheme-value*> | string | The scheme value of the specified URI |
||||

*Example*

This example finds the scheme value of the following URI:

```
uriQuery('http://www.contoso.com/catalog/shownew.htm?date=today')
```

And returns the result **http**.

<a name="utcNow"></a>

### utcNow

Return the current timestamp in an optional locale format as a string.

```
utcNow('<format>', '<locale>'?)
```

Optionally, you can specify a different format with the <*format*> parameter.


| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*format*> | No | string | A [custom format pattern](/dotnet/standard/base-types/custom-date-and-time-format-strings). The default format for the timestamp is UTC ISO format, YYYY-MM-DDTHH:mm:ss.fffZ, which complies with [ISO 8601](https://www.w3.org/QA/Tips/iso-date). |
| <*locale*> | No | string | An optional locale of culture information |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*current-timestamp*> | string | The current date and time |
||||

*Example 1*

Suppose the date is **April 15, 2018** at **1:00:00 PM**. This example gets the timestamp:

```
utcNow()
```

And returns the result **2018-04-15T13:00:00.000Z**.

*Example 2*

Suppose the date is **April 15, 2018** at **1:00:00 PM**. This example gets the current timestamp using the optional **D** format:

```
utcNow('D')
```

And returns the result **Sunday, April 15, 2018**.

*Example 3*

Suppose the date is **April 15, 2018** at **1:00:00 PM**. This example gets the current timestamp using the **de-DE** locale:

```
utcNow('', 'de-DE')
```

And returns the result **15.04.18 13:00:00**.

<a name="where"></a>

### where

Filter on each element and return the new collection of filtered elements which match a specific condition.

```
where([<collection/instance>], <iteratorName>, <function>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*collection/instance*> | Yes | array | The collection with the items |
| <*iteratorName*> | Yes | iterator name | The key item |
| <*function*> | Yes | expression | Condition function used to filter items|
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*new-collection/new-object*> | array/object | The new collection which each element has been filtered with the function  |
||||

*Example 1*

This example generates a new collection:

```
where(createArray(0, 1, 2, 3), x, x > 1)
```

And returns the result **[2, 3]**.

*Example 2*

These examples generate a new collection:

```
where(json("{'name': 'jack', 'age': '15'}"), x, x.value == 'jack')
where(json("{'name': 'jack', 'age': '15'}"), x=> x.value == 'jack')
```

And return the result **['name:jack', 'age:15']**. Note that the second expression is a *lambda expression*, which some find more readable.

<a name="xml"></a>

### xml

Return the XML version of a string that contains a JSON object.

```
xml('<value>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*value*> | Yes | string | The string with the JSON object to convert. The JSON object must have only one root property, which can't be an array. Use **\\** as an escape character for the double quotation mark (").|
||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*xml-version*> | object | The encoded XML for the specified string or JSON object |
||||

*Example 1*

This example creates the XML version for a string, which contains a JSON object:

`xml(json('{ \"name\": \"Sophia Owen\" }'))`

And returns the result XML:

```
<name>Sophia Owen</name>
```

*Example 2*

Suppose you have a `person` JSON object, seen below:

```
{
  "person": {
    "name": "Sophia Owen",
    "city": "Seattle"
  }
}
```

This example creates XML of a string that contains this JSON object:

`xml(json('{\"person\": {\"name\": \"Sophia Owen\", \"city\": \"Seattle\"}}'))`

And returns the result XML:

```
<person>
  <name>Sophia Owen</name>
  <city>Seattle</city>
<person
```

<a name="xPath"></a>

### xPath

Check XML for nodes or values that match an XPath (XML Path Language) expression, and return the matching nodes or values. An XPath expression (referred to as XPath) helps you navigate an XML document structure so that you can select nodes or compute values in the XML content.

```
xPath('<xml>', '<xpath>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*xml*> | Yes | any | The XML string to search for nodes or values that match an XPath expression value |
| <*xPath*> | Yes | any | The XPath expression used to find matching XML nodes or values |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*xml-node*> | XML | An XML node when only a single node matches the specified XPath expression |
| <*value*> | any | The value from an XML node when only a single value matches the specified XPath expression |
| <*[\<xml-node1>, \<xml-node2>, ...] -or- [\<value1>, \<value2>, ...]*> | array | An array with XML nodes or values that match the specified XPath expression |
||||

*Example 1*

This example finds nodes that match the `<name></name>` node in the specified arguments, and returns an array with those node values:

```
xPath(items, '/produce/item/name')
```

The arguments include the **items** string, which contains this XML:

```xml
"<?xml version="1.0"?> <produce> <item> <name>Gala</name> <type>apple</type> <count>20</count> </item> <item> <name>Honeycrisp</name> <type>apple</type> <count>10</count> </item> </produce>"
```

Here's the resulting array with the nodes that match `<name></name>`:

```
[ <name>Gala</name>, <name>Honeycrisp</name> ]
```

*Example 2*

Following example 1, this example finds nodes that match the `<count></count>` node and adds those node values with the [sum()](#sum) function:

```
xPath(xml(parameters('items')), 'sum(/produce/item/count)')
```

And returns the result **30**.

<a name="year"></a>

### year

Return the year of the specified timestamp.

```
year('<timestamp>')
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*timestamp*> | Yes | string | The string that contains the timestamp |
|||||

| Return value | Type | Description |
| ------------ | ---- | ----------- |
| <*year*> | integer | The year in the specified timestamp |
||||

*Example*

This example evaluates the timestamp for the year:

```
year('2018-03-15T00:00:00.000Z')
```

And it returns the result **2018**.
