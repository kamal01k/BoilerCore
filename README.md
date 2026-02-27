# BoilerCore – Modular Architecture Framework for Unity
A unity project to showcase my skill. Public Message Show. Oh nice looking msg.

✅ Featuring

- 📨 Event Bus System
- 🆕 Dependency Injection System

---

## 🚀 Event-Bus System (C# / Unity) ->

---

Designed and implemented a high-performance, modular event bus for Unity projects, combining Observer + Broadcast patterns to enable loose coupling between classes.

Programming Concept :

	GC‑Zero → shows serious design about mobile performance.
	
	Scalable → signals architectural foresight.
	
	Async + Leak‑Free → conveys modern, robust engineering.

Highlights:

    ✅ Weak References – automatic cleanup, prevents memory leaks

    ✅ Priority Levels – critical listeners (UI) execute before background tasks (analytics)

    ✅ Once Listeners – auto-remove after first invocation, ideal for initialization events

    ✅ Thread-Safe – built with ConcurrentDictionary and locks for multi-threaded environments

    ✅ Error Isolation – one failing listener doesn’t break others

    ✅ Disposable Pattern – scoped subscriptions with using statements

    ✅ Async Support – background event dispatch with SendAsync

    ✅ Zero-Alloc Send – optimized for mobile, no boxing overhead

Impact:

    Reduced coupling across game systems, improving maintainability and scalability

    Enabled GC-zero workflows for mobile games, ensuring smooth performance

    Provided a reusable, production-ready messaging framework for Unity projects

Demo Use Cases:

    UI panels subscribing/unsubscribing dynamically

    Animation controllers triggered via one-time events

    Async document loading with safe cleanup
	
Usage Examples :
	
	// 1. Define your Note classes | This is a key in pair for defining listener connection | A Practice to write clean and visible code
	public static class PDFNote
	{
		public static readonly MsgID<bool> Activate = new MsgID<bool>("PDF_Activate");
		public static readonly MsgID<Void> Hide = new MsgID<Void>("PDF_Hide");
		public static readonly MsgID<string> LoadDocument = new MsgID<string>("PDF_LoadDocument");
	}

	public static class DrawerNote
	{
		public static readonly MsgID<bool> Activate = new MsgID<bool>("Drawer_Activate");
		public static readonly MsgID<Void> Hide = new MsgID<Void>("Drawer_Hide");
	}

	public static class AnimationPanelNote
	{
		public static readonly MsgID<Animator> SetAnimator = new MsgID<Animator>("Anim_SetAnimator");
		public static readonly MsgID<bool> PlayAnimation = new MsgID<bool>("Anim_Play");
	}

	// 2. Subscribe with different features | Different ways to have listener action defined
	public class MyGameManager : MonoBehaviour
	{
		private Subscription _pdfSub;
		private Subscription _drawerSub;
		
		private void Start()
		{
			// Basic subscription
			MessageCenter.AddListener(PDFNote.Activate, OnPDFActivate);
			
			// With priority (Critical runs first)
			MessageCenter.AddListener(PDFNote.Hide, OnPDFHide, EventPriority.Critical);
			
			// Weak reference (auto-cleanup when object is GC'd)
			MessageCenter.AddListener(DrawerNote.Activate, OnDrawerActivate, 
				EventPriority.Normal, weak: true);
			
			// One-time listener (auto-remove after first call)
			MessageCenter.AddListenerOnce(AnimationPanelNote.SetAnimator, OnSetAnimator);
			
			// Using disposable pattern for automatic cleanup
			_pdfSub = MessageCenter.AddListener(PDFNote.LoadDocument, OnLoadDocument);
			_drawerSub = MessageCenter.AddListenerOnce(DrawerNote.Hide, OnDrawerHide);
		}
		
		private void OnPDFActivate(bool isActive)
		{
			Debug.Log($"PDF activated: {isActive}");
		}
		
		private void OnPDFHide()
		{
			Debug.Log("PDF hidden");
		}
		
		private void OnDrawerActivate(bool isActive)
		{
			Debug.Log($"Drawer activated: {isActive}");
		}
		
		private void OnSetAnimator(Animator animator)
		{
			Debug.Log($"Animator set: {animator.name}");
		}
		
		private void OnLoadDocument(string path)
		{
			Debug.Log($"Loading document: {path}");
		}
		
		private void OnDrawerHide()
		{
			Debug.Log("Drawer hidden once");
		}
		
		// Manual cleanup
		private void OnDestroy()
		{
			_pdfSub?.Dispose();
			_drawerSub?.Dispose();
			
			// More usal use case
			MessageCenter.RemoveListener(PDFNote.Activate);
		}
	}
	
	// 3. Sending with different features | Or Trigger event call with Send from anywhere in project
		
	// Normal sending | Void return send
	MessageCenter.Send(PDFNote.Hide);
	
	// Normal Boolean sending
	MessageCenter.Send(DrawerNote.Activate, true);
	
	// Async sending with string parameter
	private async void LoadDocumentAsync()
	{
		await MessageCenter.SendAsync(PDFNote.LoadDocument, "document.pdf");
	}
	
---

## Simple Dependency Injection (DI) System for Unity

## 🚀 Overview

A **minimal, powerful, and clean Dependency Injection system** built specifically for Unity.

Designed for developers who want:

- No heavy frameworks  
- No reflection overhead  
- No complex installers  
- Full runtime control  
- Clean & scalable architecture  

This system gives you professional-grade DI — without overengineering.

---

# ✨ Core Features

## 🏷 Named Bindings

Bind multiple implementations of the same interface.

```csharp
DI.Container.BindNamed<IWeapon>("sword", () => new Sword());
DI.Container.BindNamed<IWeapon>("bow", () => new Bow());

var sword = DI.Container.ResolveNamed<IWeapon>("sword");
```

✔ Perfect for weapons, abilities, AI strategies

---

## 🧩 Singleton Pattern Support

Create and reuse a single instance safely.

```csharp
DI.Container.BindSingleton<IGameSettings>(() => new GameSettings());
```

✔ Memory efficient  
✔ Fast access  
✔ Safe global services  

---

## 🛡 Safe Resolution

Prevent crashes on missing bindings.

```csharp
var service = DI.Container.ResolveOrDefault<IMyService>();

if (DI.Container.TryResolve<IMyService>(out var service))
{
    // Safe to use
}
```

✔ Production-safe  
✔ No runtime exceptions  

---

## 🔄 Full Binding Management

```csharp
DI.Container.RemoveBinding<IMyService>();
DI.Container.RemoveNamedBinding("sword");

DI.Container.ClearAllBindings();

bool hasService = DI.Container.HasBinding<IMyService>();
int totalBindings = DI.Container.BindingCount;
```

✔ Runtime flexibility  
✔ Scene transition support  
✔ Clean test teardown  

---

## 🔍 Debug Utilities

```csharp
var allTypes = DI.Container.GetAllBoundTypes();
```

✔ Inspect all registered services  
✔ Great for debugging large systems  

---

# 🧠 Architecture

```
Core/
 └── DI/
      ├── DI.cs
      └── DIContainer.cs
```

### 🔹 DI (Static Entry Point)

- Global container access
- Auto-initialized before scene load
- Not a MonoBehaviour

### 🔹 DIContainer

Handles:

- Type bindings  
- Named bindings  
- Singleton caching  
- Safe resolution  
- Runtime binding management  

Internally optimized using:

- `Dictionary<Type, Func<object>>`
- `Dictionary<Type, object>`
- `Dictionary<string, Type>`

Fast. Clean. Lightweight.

---

# 🎯 Why Use This Instead of Large Frameworks?

| Feature | This DI | Heavy DI Frameworks |
|----------|---------|--------------------|
| Setup Complexity | ⭐ Very Low | High |
| Reflection Usage | ❌ None | Often Yes |
| Performance | ⚡ Fast | Moderate |
| Learning Curve | Easy | Steep |
| Runtime Control | Full | Limited |

Perfect for indie, mid-size, and scalable Unity projects.

---

# 🛠 Example Usage

### 🔹 Bootstrapping

```csharp
DI.Container.BindSingleton<IGameSettings>(() => new GameSettings());
DI.Container.Bind<IEnemyAI>(() => new BasicEnemyAI());
```

### 🔹 Resolving

```csharp
var settings = DI.Container.Resolve<IGameSettings>();
var ai = DI.Container.Resolve<IEnemyAI>();
```

Clean. Simple. Controlled.

---

# 📦 Use Cases

- 🎮 Weapon systems (Sword / Bow / Gun)
- 🧠 AI behavior swapping
- 🔊 Audio services
- 🌐 Networking layers
- 💾 Save systems
- 🧪 Unit testing with mock services
- ⚙ Game configuration systems

---

# 🔮 Extensible Design

Easily extend with:

- Scoped containers  
- Constructor injection  
- Attribute injection  
- Editor debugging tools  
- Scene-based containers  

Built to grow with your architecture.

---

# 📊 Performance

- O(1) resolution lookup  
- Singleton caching  
- No reflection  
- No runtime scanning  
- Minimal memory footprint  

Optimized for runtime-critical environments.

---

# 🏆 Perfect For

✔ Indie Developers  
✔ Clean Code Enthusiasts  
✔ Modular Architecture Lovers  
✔ Unity Professionals  
✔ Scalable Projects  

---

# 📜 License

Unlicense License – Free to use, modify, and distribute.

---

# ⭐ Support

If you find this useful:

- ⭐ Star the repository  
- 🍴 Fork it  
- 🧠 Improve it  
- 🚀 Use it in production  

---

<p align="center">
  Built with ❤️ for Unity Developers
</p>

## 🔔 Latest Update

### v1.1.0 – Dependency Injection System Added

➕ Added lightweight DI container  
➕ Named bindings support  
➕ Singleton caching  
➕ Safe resolution methods  
➕ Runtime binding management  