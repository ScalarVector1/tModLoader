using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI.Chat;

namespace Terraria.ModLoader.UI;

// UITextPanel except we scale and manipulate Text to preserve original dimensions.
public class UIAutoScaleTextButton<T> : UIAutoScaleTextTextPanel<T>
{
	private static Asset<Texture2D> PanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");

	public UIAutoScaleTextButton(T text, float textScaleMax = 1f, bool large = false) : base(text, textScaleMax, large)
	{
	}

	private void DrawButtonPanel(SpriteBatch spriteBatch)
	{
		var position = GetDimensions().Position();
		var width = GetDimensions().Width;
		var height = GetDimensions().Height;

		spriteBatch.Draw(PanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle(8, 0, 8, (int)height - 8), BackgroundColor, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
		spriteBatch.Draw(PanelTexture.Value, new Vector2(position.X + 8f, position.Y + height - 8), new Rectangle(8, PanelTexture.Height() - 8, 8, 8), BackgroundColor, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);

		spriteBatch.Draw(PanelTexture.Value, position, new Rectangle(0, 0, 8, (int)height - 8), BackgroundColor);
		spriteBatch.Draw(PanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle(PanelTexture.Width() - 8, 0, 8, (int)height - 8), BackgroundColor);

		spriteBatch.Draw(PanelTexture.Value, position + Vector2.UnitY * (height - 8), new Rectangle(0, PanelTexture.Height() - 8, 8, 8), BackgroundColor);
		spriteBatch.Draw(PanelTexture.Value, new Vector2(position.X + width - 8f, position.Y + height - 8), new Rectangle(PanelTexture.Width() - 8, PanelTexture.Height() - 8, 8, 8), BackgroundColor);
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		if (float.IsNaN(TextScale))
			Recalculate();

		if (DrawPanel)
			DrawButtonPanel(spriteBatch);

		var innerDimensions = UseInnerDimensions ? GetInnerDimensions().ToRectangle() : GetDimensions().ToRectangle();
		if (UseInnerDimensions)
			innerDimensions.Inflate(0, 6);
		else
			innerDimensions.Inflate(-4, -8);

		for (int i = 0; i < textStrings.Length; i++) {
			//Vector2 pos = innerDimensions.Center.ToVector2() + drawOffsets[i];
			Vector2 pos = innerDimensions.TopLeft() + drawOffsets[i];
			if (IsLarge)
				Utils.DrawBorderStringBig(spriteBatch, textStrings[i], pos, TextColor, TextScale, 0f, 0f, -1);
			else
				Utils.DrawBorderString(spriteBatch, textStrings[i], pos, TextColor, TextScale, 0f, 0f, -1);
		}
	}
}