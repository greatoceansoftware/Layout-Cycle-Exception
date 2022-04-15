# Layout-Cycle-Exception
This demonstrates a layout cycle exception in .NET MAUI RC1.

Steps to duplicate issue:

1. Build and run the project (debug mode) as Windows Machine platform.
2. Click the "Do Something Button." The Activities in the list will be filtered (hidden).
3. Try to resize the Window.
4. Unhandled exception occurs.
