using System.Collections;
using Microsoft.Maui.Graphics.Converters;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SKPaintSurfaceEventArgs = SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;

/// <summary>
/// A control that allows the user to pick a <see cref="Color"/>.
/// </summary>
public partial class CustomColorPicker : ContentView
{
    private Color? pendingPickedColor = null;
    private bool rendering = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomColorPicker"/> class.
    /// </summary>
    public CustomColorPicker()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Occurs when the Picked Color changes
    /// </summary>
    public event EventHandler<PickedColorChangedEventArgs>? PickedColorChanged;

    /// <summary>
    /// Checks whether this <see cref="CustomColorPicker"/> is in a rendering state.
    /// A <see cref="CustomColorPicker"/> in a rendering state cannot have its properties
    /// modified by outside code.
    /// </summary>
    public bool IsRendering => rendering;

    /// <summary>
    /// Backing store for the <see cref="PickedColor"/> property.
    /// </summary>
    public static readonly BindableProperty PickedColorProperty = BindableProperty.Create(
        nameof(PickedColor),
        typeof(Color),
        typeof(CustomColorPicker),
        propertyChanged: (bindable, value, newValue) =>
        {
            if (!newValue.Equals(value) && (bindable is CustomColorPicker picker))
            {
                picker.PickedColorChanged?.Invoke(
                    picker,
                    new PickedColorChangedEventArgs((Color?)value, (Color)newValue)
                );
                if (!picker.rendering)
                {
                    picker.pendingPickedColor = (Color)newValue;
                    picker.CanvasView.InvalidateSurface();
                }
            }
        }
    );

    /// <summary>
    /// Gets and sets the current picked <see cref="Color"/>. This is a bindable property.
    /// </summary>
    /// <value>
    /// A <see cref="Color"/> containing the picked color. The default value is <see langword="null"/>.
    /// </value>
    /// <remarks>
    /// Setting this value to <see langword="null"/> makes the control honor the values set
    /// to <see cref="PointerRingPositionXUnits"/> and <see cref="PointerRingPositionYUnits"/>
    /// instead.
    /// <br/>
    /// Setting this property will cause the <see cref="PickedColorChanged"/> event to be emitted.
    /// </remarks>
    public Color PickedColor
    {
        get { return (Color)GetValue(PickedColorProperty); }
        set
        {
            if (!rendering)
            {
                SetValue(PickedColorProperty, value);
            }
        }
    }

    /// <summary>
    /// Backing store for the <see cref="ColorSpectrumStyle"/> property.
    /// </summary>
    public static readonly BindableProperty ColorSpectrumStyleProperty = BindableProperty.Create(
        nameof(ColorSpectrumStyle),
        typeof(ColorSpectrumStyle),
        typeof(CustomColorPicker),
        ColorSpectrumStyle.HueToShadeStyle,
        BindingMode.Default,
        null,
        propertyChanged: (bindable, value, newValue) =>
        {
            if (newValue != null)
                ((CustomColorPicker)bindable).CanvasView.InvalidateSurface();
            else
                ((CustomColorPicker)bindable).ColorSpectrumStyle = default;
        }
    );

    /// <summary>
    /// Gets or sets the Color Spectrum Gradient Style.
    /// </summary>
    public ColorSpectrumStyle ColorSpectrumStyle
    {
        get { return (ColorSpectrumStyle)GetValue(ColorSpectrumStyleProperty); }
        set { SetValue(ColorSpectrumStyleProperty, value); }
    }

    /// <summary>
    /// Backing store for the <see cref="BaseColorList"/> property.
    /// </summary>
    public static readonly BindableProperty BaseColorListProperty = BindableProperty.Create(
        nameof(BaseColorList),
        typeof(IEnumerable),
        typeof(CustomColorPicker),
        new string[]
        {
            new Color(255, 0, 0).ToHex(), // Red
            new Color(255, 255, 0).ToHex(), // Yellow
            new Color(0, 255, 0).ToHex(), // Green (Lime)
            new Color(0, 255, 255).ToHex(), // Aqua
            new Color(0, 0, 255).ToHex(), // Blue
            new Color(255, 0, 255).ToHex(), // Fuchsia
            new Color(255, 0, 0).ToHex(), // Red
        },
        BindingMode.Default,
        null,
        propertyChanged: (bindable, value, newValue) =>
        {
            if (newValue != null)
                ((CustomColorPicker)bindable).CanvasView.InvalidateSurface();
            else
                ((CustomColorPicker)bindable).BaseColorList = Array.Empty<string>();
        }
    );

    /// <summary>
    /// Gets or sets the Base Color List.
    /// </summary>
    public IEnumerable BaseColorList
    {
        get { return (IEnumerable)GetValue(BaseColorListProperty); }
        set { SetValue(BaseColorListProperty, value); }
    }

    /// <summary>
    /// Backing store for the <see cref="ColorFlowDirection"/> property.
    /// </summary>
    public static readonly BindableProperty ColorFlowDirectionProperty = BindableProperty.Create(
        nameof(ColorFlowDirection),
        typeof(ColorFlowDirection),
        typeof(CustomColorPicker),
        ColorFlowDirection.Horizontal,
        BindingMode.Default,
        null,
        propertyChanged: (bindable, value, newValue) =>
        {
            if (newValue != null)
                ((CustomColorPicker)bindable).CanvasView.InvalidateSurface();
            else
                ((CustomColorPicker)bindable).ColorFlowDirection = default;
        }
    );

    /// <summary>
    /// Gets or sets the Color List flow Direction.
    /// </summary>
    /// <value>
    /// Either <see cref="ColorFlowDirection.Horizontal"/> or <see cref="ColorFlowDirection.Vertical"/>.
    /// </value>
    public ColorFlowDirection ColorFlowDirection
    {
        get { return (ColorFlowDirection)GetValue(ColorFlowDirectionProperty); }
        set { SetValue(ColorFlowDirectionProperty, value); }
    }

    /// <summary>
    /// Backing store for the <see cref="PointerRingDiameterUnits"/> property.
    /// </summary>
    public static readonly BindableProperty PointerRingDiameterUnitsProperty =
        BindableProperty.Create(
            nameof(PointerRingDiameterUnits),
            typeof(double),
            typeof(CustomColorPicker),
            0.6,
            BindingMode.Default,
            validateValue: (bindable, value) =>
            {
                return (((double)value > -1) && ((double)value <= 1));
            },
            propertyChanged: (bindable, value, newValue) =>
            {
                if (newValue != null)
                    ((CustomColorPicker)bindable).CanvasView.InvalidateSurface();
                else
                    ((CustomColorPicker)bindable).PointerRingDiameterUnits = default;
            }
        );

    /// <summary>
    /// Gets or sets the Picker Pointer Ring Diameter.
    /// The size is calculated relative to the view canvas size.
    /// </summary>
    /// <value>
    /// A number between 0 - 1.
    /// </value>
    public double PointerRingDiameterUnits
    {
        get { return (double)GetValue(PointerRingDiameterUnitsProperty); }
        set { SetValue(PointerRingDiameterUnitsProperty, value); }
    }

    /// <summary>
    /// Backing store for the <see cref="PointerRingBorderUnits"/> property.
    /// </summary>
    public static readonly BindableProperty PointerRingBorderUnitsProperty =
        BindableProperty.Create(
            nameof(PointerRingBorderUnits),
            typeof(double),
            typeof(CustomColorPicker),
            0.3,
            BindingMode.Default,
            validateValue: (bindable, value) =>
            {
                return (((double)value > -1) && ((double)value <= 1));
            },
            propertyChanged: (bindable, value, newValue) =>
            {
                if (newValue != null)
                    ((CustomColorPicker)bindable).CanvasView.InvalidateSurface();
                else
                    ((CustomColorPicker)bindable).PointerRingBorderUnits = default;
            }
        );

    /// <summary>
    /// Gets or sets the Picker Pointer Ring Border Size.
    /// The size is calculated relative to the pixel size of the picker pointer.
    /// </summary>
    /// <value>
    /// A number between 0 - 1.
    /// </value>
    public double PointerRingBorderUnits
    {
        get { return (double)GetValue(PointerRingBorderUnitsProperty); }
        set { SetValue(PointerRingBorderUnitsProperty, value); }
    }

    /// <summary>
    /// Backing store for the <see cref="PointerRingPositionXUnits"/> property.
    /// </summary>
    public static readonly BindableProperty PointerRingPositionXUnitsProperty =
        BindableProperty.Create(
            nameof(PointerRingPositionXUnits),
            typeof(double),
            typeof(CustomColorPicker),
            0.5,
            BindingMode.OneTime,
            validateValue: (bindable, value) =>
            {
                return (((double)value > -1) && ((double)value <= 1));
            },
            propertyChanged: (bindable, value, newValue) =>
            {
                if (
                    (double)newValue != (double)value
                    && bindable is CustomColorPicker picker
                    && !picker.rendering
                )
                {
                    picker.pendingPickedColor = null;
                    picker.CanvasView.InvalidateSurface();
                }
            }
        );

    /// <summary>
    /// Gets or sets the Picker Pointer X position.
    /// The value is calculated relative to the view canvas width.
    /// </summary>
    /// <value>
    /// A number between 0 - 1.
    /// </value>
    public double PointerRingPositionXUnits
    {
        get { return (double)GetValue(PointerRingPositionXUnitsProperty); }
        set
        {
            if (!rendering)
            {
                SetValue(PointerRingPositionXUnitsProperty, value);
            }
        }
    }

    /// <summary>
    /// Backing store for the <see cref="PointerRingPositionYUnits"/> property.
    /// </summary>
    public static readonly BindableProperty PointerRingPositionYUnitsProperty =
        BindableProperty.Create(
            nameof(PointerRingPositionYUnits),
            typeof(double),
            typeof(CustomColorPicker),
            0.5,
            BindingMode.OneTime,
            validateValue: (bindable, value) =>
            {
                return (((double)value > -1) && ((double)value <= 1));
            },
            propertyChanged: (bindable, value, newValue) =>
            {
                if (
                    (double)newValue != (double)value
                    && bindable is CustomColorPicker picker
                    && !picker.rendering
                )
                {
                    picker.pendingPickedColor = null;
                    picker.CanvasView.InvalidateSurface();
                }
            }
        );

    /// <summary>
    /// Gets or sets the Picker Pointer Y position.
    /// The value is calculated relative to the view canvas height.
    /// </summary>
    /// <value>
    /// A number between 0 - 1.
    /// </value>
    public double PointerRingPositionYUnits
    {
        get { return (double)GetValue(PointerRingPositionYUnitsProperty); }
        set
        {
            if (!rendering)
            {
                SetValue(PointerRingPositionYUnitsProperty, value);
            }
        }
    }

    private void CanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        rendering = true;

        var skImageInfo = e.Info;
        var skSurface = e.Surface;
        var skCanvas = skSurface.Canvas;

        var skCanvasWidth = skImageInfo.Width;
        var skCanvasHeight = skImageInfo.Height;

        skCanvas.Clear(SKColors.White);

        // Draw gradient rainbow Color spectrum
        using (var paint = new SKPaint())
        {
            paint.IsAntialias = true;

            // Initiate the base Color list
            ColorTypeConverter converter = new ColorTypeConverter();
            var colors = BaseColorList
                .Cast<object>()
                .Select(color =>
                    converter.ConvertFromInvariantString(color?.ToString() ?? string.Empty)
                )
                .Where(color => color != null)
                .Cast<Color>()
                .Select(color => color.ToSKColor())
                .ToList();

            // create the gradient shader between base Colors
            using (
                var shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    ColorFlowDirection is ColorFlowDirection.Horizontal
                        ? new SKPoint(skCanvasWidth, 0)
                        : new SKPoint(0, skCanvasHeight),
                    colors.ToArray(),
                    null,
                    SKShaderTileMode.Clamp
                )
            )
            {
                paint.Shader = shader;
                skCanvas.DrawPaint(paint);
            }
        }

        // Draw secondary gradient color spectrum
        using (var paint = new SKPaint())
        {
            paint.IsAntialias = true;

            // Initiate gradient color spectrum style layer
            var colors = GetSecondaryLayerColors(ColorSpectrumStyle);

            // create the gradient shader between secondary colors
            using (
                var shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    ColorFlowDirection is ColorFlowDirection.Horizontal
                        ? new SKPoint(0, skCanvasHeight)
                        : new SKPoint(skCanvasWidth, 0),
                    colors,
                    null,
                    SKShaderTileMode.Clamp
                )
            )
            {
                paint.Shader = shader;
                skCanvas.DrawPaint(paint);
            }
        }

        SKPoint touchPoint;
        // Represent the color of the current Touch point
        SKColor touchPointColor;

        if (pendingPickedColor is null)
        {
            // The user hasn't explicitly specified the touchPoint color.
            // The touchPoint can therefore be calculated quickly.
            touchPoint = new SKPoint(
                x: skCanvasWidth * (float)PointerRingPositionXUnits,
                y: skCanvasHeight * (float)PointerRingPositionYUnits
            );
            // Picking the Pixel Color values on the Touch Point

            // Efficient and fast
            // https://forums.xamarin.com/discussion/92899/read-a-pixel-info-from-a-canvas
            // create the 1x1 bitmap (auto allocates the pixel buffer)
            using (SKBitmap bitmap = new SKBitmap(skImageInfo))
            {
                // get the pixel buffer for the bitmap
                IntPtr dstpixels = bitmap.GetPixels();

                // read the surface into the bitmap
                skSurface.ReadPixels(
                    skImageInfo,
                    dstpixels,
                    skImageInfo.RowBytes,
                    (int)touchPoint.X,
                    (int)touchPoint.Y
                );

                // access the color
                touchPointColor = bitmap.GetPixel(0, 0);
            }

            // Set selected color
            SetValue(PickedColorProperty, touchPointColor.ToMauiColor());
        }
        else
        {
            // We'll have to brute force the board to find the nearest color.
            touchPointColor = pendingPickedColor.ToSKColor();
            using var bitmap = new SKBitmap(skImageInfo);
            var dstpixels = bitmap.GetPixels();
            skSurface.ReadPixels(skImageInfo, dstpixels, skImageInfo.RowBytes, 0, 0);

            int desiredX = -1;
            int desiredY = -1;
            int nearestDesiredX = -1;
            int nearestDesiredY = -1;
            int distance = int.MaxValue;

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    var currentColor = bitmap.GetPixel(x, y);
                    if (currentColor == touchPointColor)
                    {
                        desiredX = x;
                        desiredY = y;
                        goto found;
                    }
                    else
                    {
                        var currentDistance =
                            Math.Abs(currentColor.Red - touchPointColor.Red)
                            + Math.Abs(currentColor.Green - touchPointColor.Green)
                            + Math.Abs(currentColor.Blue - touchPointColor.Blue)
                            + Math.Abs(currentColor.Alpha - touchPointColor.Alpha);

                        if (currentDistance < distance)
                        {
                            distance = currentDistance;
                            nearestDesiredX = x;
                            nearestDesiredY = y;
                        }
                    }
                }
            }
            found:
            if (desiredX != -1 && desiredY != -1)
            {
                touchPoint = new SKPoint(desiredX, desiredY);
            }
            else
            {
                touchPoint = new SKPoint(nearestDesiredX, nearestDesiredY);
            }

            // Set pointer position.
            SetValue(PointerRingPositionXUnitsProperty, (double)touchPoint.X / skCanvasWidth);
            SetValue(PointerRingPositionYUnitsProperty, (double)touchPoint.Y / skCanvasHeight);
        }

        // Painting the Touch point
        using (SKPaint paintTouchPoint = new SKPaint())
        {
            paintTouchPoint.Style = SKPaintStyle.Fill;
            paintTouchPoint.Color = SKColors.White;
            paintTouchPoint.IsAntialias = true;

            var canvasLongestLength =
                (skCanvasWidth > skCanvasHeight) ? skCanvasWidth : skCanvasHeight;

            // Calculate 1/10th of the units value for scaling
            var pointerRingDiameterUnitsScaled = (float)PointerRingDiameterUnits / 10f;
            // Calculate against Longest Length of Canvas
            var pointerRingDiameter = (float)canvasLongestLength * pointerRingDiameterUnitsScaled;

            // Outer circle of the Pointer (Ring)
            skCanvas.DrawCircle(
                touchPoint.X,
                touchPoint.Y,
                (pointerRingDiameter / 2),
                paintTouchPoint
            );

            // Draw another circle with picked color
            paintTouchPoint.Color = touchPointColor;

            // Calculate against Pointer Circle
            var pointerRingInnerCircleDiameter =
                (float)pointerRingDiameter * (float)PointerRingBorderUnits;

            // Inner circle of the Pointer (Ring)
            skCanvas.DrawCircle(
                touchPoint.X,
                touchPoint.Y,
                ((pointerRingDiameter - pointerRingInnerCircleDiameter) / 2),
                paintTouchPoint
            );
        }

        rendering = false;
    }

    private void CanvasView_OnTouch(object sender, SKTouchEventArgs e)
    {
#if WINDOWS
        if (!e.InContact)
            return;
#endif

        var canvasSize = CanvasView.CanvasSize;

        // Check for each touch point XY position to be inside Canvas
        // Ignore any Touch event occured outside the Canvas region
        if (
            (e.Location.X > 0 && e.Location.X < canvasSize.Width)
            && (e.Location.Y > 0 && e.Location.Y < canvasSize.Height)
        )
        {
            e.Handled = true;

            pendingPickedColor = null;

            // Prevent double re-rendering.
            rendering = true;
            SetValue(PointerRingPositionXUnitsProperty, e.Location.X / canvasSize.Width);
            SetValue(PointerRingPositionYUnitsProperty, e.Location.Y / canvasSize.Height);
            rendering = false;

            // Explicitly render things now.
            CanvasView.InvalidateSurface();
        }
    }

    private SKColor[] GetSecondaryLayerColors(ColorSpectrumStyle colorSpectrumStyle)
    {
        switch (colorSpectrumStyle)
        {
            case ColorSpectrumStyle.HueOnlyStyle:
                return new SKColor[] { SKColors.Transparent };
            case ColorSpectrumStyle.HueToShadeStyle:
                return new SKColor[] { SKColors.Transparent, SKColors.Black };
            case ColorSpectrumStyle.ShadeToHueStyle:
                return new SKColor[] { SKColors.Black, SKColors.Transparent };
            case ColorSpectrumStyle.HueToTintStyle:
                return new SKColor[] { SKColors.Transparent, SKColors.White };
            case ColorSpectrumStyle.TintToHueStyle:
                return new SKColor[] { SKColors.White, SKColors.Transparent };
            case ColorSpectrumStyle.TintToHueToShadeStyle:
                return new SKColor[] { SKColors.White, SKColors.Transparent, SKColors.Black };
            case ColorSpectrumStyle.ShadeToHueToTintStyle:
                return new SKColor[] { SKColors.Black, SKColors.Transparent, SKColors.White };
            default:
                return new SKColor[] { SKColors.Transparent, SKColors.Black };
        }
    }
}

/// <summary>
/// Represents the event arguments for the PickedColorChanged event.
/// </summary>
public class PickedColorChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PickedColorChangedEventArgs"/> class.
    /// </summary>
    /// <param name="oldColor">The old color value.</param>
    /// <param name="newColor">The new color value.</param>
    public PickedColorChangedEventArgs(Color oldColor, Color newColor)
    {
        OldColor = oldColor;
        NewColor = newColor;
    }

    /// <summary>
    /// Gets the old color value.
    /// </summary>
    public Color OldColor { get; private set; }

    /// <summary>
    /// Gets the new color value.
    /// </summary>
    public Color NewColor { get; private set; }
}

/// <summary>
/// Enumerate values that describe color spectrum styles of a <see cref="CustomColorPicker"/>.
/// </summary>
public enum ColorSpectrumStyle
{
    /// <summary>
    /// Hue only style.
    /// </summary>
    HueOnlyStyle,

    /// <summary>
    /// Hue to shade style.
    /// </summary>
    HueToShadeStyle,

    /// <summary>
    /// Shade to hue style.
    /// </summary>
    ShadeToHueStyle,

    /// <summary>
    /// Hue to tint style.
    /// </summary>
    HueToTintStyle,

    /// <summary>
    /// Tint to hue style.
    /// </summary>
    TintToHueStyle,

    /// <summary>
    /// Tint to hue to shade style.
    /// </summary>
    TintToHueToShadeStyle,

    /// <summary>
    /// Shade to hue to tint style.
    /// </summary>
    ShadeToHueToTintStyle
}

/// <summary>
/// Enumerate values that describe color flow directions of a <see cref="CustomColorPicker"/>.
/// </summary>
public enum ColorFlowDirection
{
    /// <summary>
    /// Indicates that the colors will flow horizontally.
    /// </summary>
    Horizontal,

    /// <summary>
    /// Indicates that the colors will flow vertically.
    /// </summary>
    Vertical
}
