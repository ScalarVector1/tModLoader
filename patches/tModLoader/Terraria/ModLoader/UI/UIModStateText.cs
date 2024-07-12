using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.ModLoader.UI;

internal class UIModStateText : UIElement
{
	private bool _enabled;

	private static Asset<Texture2D> PanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");

	private string DisplayText
		=> _enabled
			? Language.GetTextValue("GameUI.Enabled")
			: Language.GetTextValue("GameUI.Disabled");

	private Color DisplayColor
		=> _enabled ? UIColors.enabledGreen : UIColors.disabledGray;

	private Color TextColor
		=> IsMouseHovering ? Color.Yellow : Color.White;

	public UIModStateText(bool enabled = true)
	{
		_enabled = enabled;
		PaddingLeft = PaddingRight = 8f;
		PaddingBottom = PaddingTop = 10f;
	}

	public void SetEnabled()
	{
		_enabled = true;
		Recalculate();
	}

	public void SetDisabled()
	{
		_enabled = false;
		Recalculate();
	}

	public override void Recalculate()
	{
		var textSize = new Vector2(FontAssets.MouseText.Value.MeasureString(DisplayText).X, 16f);
		Width.Set(textSize.X + PaddingLeft + PaddingRight, 0f);
		Height.Set(textSize.Y + PaddingTop + PaddingBottom, 0f);
		base.Recalculate();
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		base.DrawSelf(spriteBatch);
		DrawPanel(spriteBatch);
		DrawEnabledText(spriteBatch);
	}

	private void DrawPanel(SpriteBatch spriteBatch)
	{
		var position = GetDimensions().Position();
		var width = Width.Pixels;
		var height = Height.Pixels;

		spriteBatch.Draw(PanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle(8, 0, 8, (int)height - 8), DisplayColor, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
		spriteBatch.Draw(PanelTexture.Value, new Vector2(position.X + 8f, position.Y + height - 8), new Rectangle(8, PanelTexture.Height() - 8, 8, 8), DisplayColor, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);

		spriteBatch.Draw(PanelTexture.Value, position, new Rectangle(0, 0, 8, (int)height - 8), DisplayColor);
		spriteBatch.Draw(PanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle(PanelTexture.Width() - 8, 0, 8, (int)height -8), DisplayColor);

		spriteBatch.Draw(PanelTexture.Value, position + Vector2.UnitY * (height - 8), new Rectangle(0, PanelTexture.Height() - 8, 8, 8), DisplayColor);
		spriteBatch.Draw(PanelTexture.Value, new Vector2(position.X + width - 8f, position.Y + height - 8), new Rectangle(PanelTexture.Width() - 8, PanelTexture.Height() - 8, 8, 8), DisplayColor);
	}

	private void DrawEnabledText(SpriteBatch spriteBatch)
	{
		var pos = GetDimensions().Position() + new Vector2(PaddingLeft, PaddingTop * 0.75f);
		Utils.DrawBorderString(spriteBatch, DisplayText, pos, TextColor);
	}
}