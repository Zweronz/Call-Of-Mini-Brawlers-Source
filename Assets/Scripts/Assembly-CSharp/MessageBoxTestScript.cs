using UnityEngine;

public class MessageBoxTestScript : MonoBehaviour
{
	private MessageBox messageBox;

	private void OnGUI()
	{
		if (GUILayout.Button("Show MessageBox"))
		{
			if (messageBox == null)
			{
				messageBox = new MessageBox("TestMessageBox", "This is the test Message. \r\n                     This is the test Message.\r\n                     This is the test Message.\r\n                     This is the test Message.", HandleMessageBoxBtnClick, "OK", "Cancel");
			}
			messageBox.Show();
			Application.LoadLevel("MessageBoxTestBlankScene");
		}
	}

	private void HandleMessageBoxBtnClick(MessageBox messageBox, int buttonIndex)
	{
		switch (buttonIndex)
		{
		case 0:
			Debug.Log("Clicked OK");
			break;
		case 1:
			Debug.Log("Clicked Cancel");
			break;
		}
	}
}
