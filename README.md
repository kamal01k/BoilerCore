# BoilerCore
A unity project to showcase my skill. Public Message Show. Oh nice looking msg.

Event-Bus System (C# / Unity) ->

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
	
