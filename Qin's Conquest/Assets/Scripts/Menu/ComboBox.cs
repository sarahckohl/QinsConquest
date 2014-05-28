using UnityEngine;

// Script for dropdown menus
// From http://wiki.unity3d.com/index.php?title=PopupList
// The code is the one at the very bottom
// My changes:
// Formated code differently to fit my style
// Changed the droptdown menu to show on top of button if list goes beyond screen height

// Popup list created by Eric Haines
// ComboBox Extended by Hyungseok Seo.(Jerry) sdragoon@nate.com
// Refactored by zhujiangbo jumbozhu@gmail.com
// Slight edit for button to show the previously selected item AndyMartin458 www.clubconsortya.blogspot.com

public class ComboBox {
	private static bool forceToUnShow = false; 
	private static int useControlID = -1;
	private bool isClickedComboButton = false;
	private int selectedItemIndex = 0;
	
	private Rect rect;
	private GUIContent buttonContent;
	private GUIContent[] listContent;
	private string buttonStyle;
	private string boxStyle;
	private GUIStyle listStyle;
	private bool done;
	private int controlID;
	
	public ComboBox( Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle ) {
		this.rect = rect;
		this.buttonContent = buttonContent;
		this.listContent = listContent;
		this.buttonStyle = "button";
		this.boxStyle = "box";
		this.listStyle = listStyle;
	}
	
	public ComboBox(Rect rect, GUIContent buttonContent, GUIContent[] listContent, string buttonStyle, string boxStyle, GUIStyle listStyle) {
		this.rect = rect;
		this.buttonContent = buttonContent;
		this.listContent = listContent;
		this.buttonStyle = buttonStyle;
		this.boxStyle = boxStyle;
		this.listStyle = listStyle;
	}
	
	public int Show() {
		if(forceToUnShow) {
			forceToUnShow = false;
			isClickedComboButton = false;
		}
		
		done = false;
		controlID = GUIUtility.GetControlID(FocusType.Passive);       
		
		switch( Event.current.GetTypeForControl(controlID)) {
			case EventType.mouseUp:
			{
				if( isClickedComboButton ) {
					done = true;
				}
			}
			break;
		}
		
		
		if(GUI.Button(rect, buttonContent, buttonStyle)) {
			if( useControlID == -1 ) {
				useControlID = controlID;
				isClickedComboButton = false;
			}
			
			if(useControlID != controlID) {
				forceToUnShow = true;
				useControlID = controlID;
			}
			isClickedComboButton = true;
		}
		
		if(isClickedComboButton) {
			Rect listRect;
			
			if ((rect.y + rect.height + listStyle.CalcHeight(listContent[0], 1.0f) * listContent.Length) > Screen.height) {
				listRect = new Rect( rect.x, rect.y - listStyle.CalcHeight(listContent[0], 1.0f) * listContent.Length,
			    		rect.width, listStyle.CalcHeight(listContent[0], 1.0f) * listContent.Length );
		 	} else {
				listRect = new Rect(rect.x, rect.y + rect.height,
				                    rect.width, listStyle.CalcHeight(listContent[0], 1.0f) * listContent.Length );
		 	}
			
			GUI.Box(listRect, "", boxStyle);
			int newSelectedItemIndex = GUI.SelectionGrid( listRect, selectedItemIndex, listContent, 1, listStyle );
			if(newSelectedItemIndex != selectedItemIndex) {
				selectedItemIndex = newSelectedItemIndex;
				buttonContent = listContent[selectedItemIndex];
			}
		}
		
		if(done)
			isClickedComboButton = false;
		
		return selectedItemIndex;
	}
	
	public int SelectedItemIndex {
		get {
			return selectedItemIndex;
		}
		set {
			selectedItemIndex = value;
		}
	}
}