var mystack = new MyStack<object>();

mystack.OnPush += () => Console.WriteLine("OnPush");
mystack.OnPop += () => Console.WriteLine("OnPop");
mystack.OnClear += () => Console.WriteLine("OnClear");

mystack.Push(1);
mystack.Push(2);
mystack.Push(3);

System.Console.WriteLine(mystack.Pop());
System.Console.WriteLine(mystack.Pop());
System.Console.WriteLine(mystack.Pop());