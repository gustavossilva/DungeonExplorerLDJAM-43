using UnityEngine;

public enum Effect{
	RESTAURA_HP,
	ACAO_DUPLA,
	REDUZ_DANO_RECEBIDO,
	REMOVE_COOLDOWN,
	AUMENTA_DANO
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {

	public Sprite _spriteInGame;
	public Sprite _spriteUI;
	public Effect _effect;
}
