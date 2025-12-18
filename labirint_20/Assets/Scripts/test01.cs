using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LLMUnity;

public class test01 {
  public LLMCharacter llmCharacter;
  
  void HandleReply(string reply){
    // do something with the reply from the model
    Debug.Log(reply);
  }
  
  void Game(){
    // your game function
    string message = "Hello bot!";
    Debug.Log(llmCharacter.Chat(message, HandleReply));
  }
}