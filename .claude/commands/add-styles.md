# Add Styles

Generate an antd-style CSS-in-JS style file using `createStyles()` with Ant Design design tokens for consistent theming.

## Arguments

`$ARGUMENTS` — the component or page name (e.g. `InvoiceCard`, `DashboardHeader`, `MetricPanel`). If not provided, ask the user for it.

## Instructions

Parse `$ARGUMENTS` to extract:
- **ComponentName**: PascalCase name of the component being styled (e.g. `InvoiceCard`)
- **componentName**: camelCase (e.g. `invoiceCard`)

Before generating, ask the user:
1. What CSS class names are needed? (e.g. container, header, title, body, footer, badge)
2. For each class, what CSS properties? (layout, spacing, typography, colors, borders, shadows, etc.)
3. Should any classes use Ant Design design tokens? (borderRadius, colorPrimary, colorBgContainer, spacing, etc.)
4. Where should this file live? (default: `src/components/{feature}/style/{ComponentName}.style.ts`)

Key Ant Design tokens available via `token`:
- **Colors**: `token.colorPrimary`, `token.colorSuccess`, `token.colorWarning`, `token.colorError`, `token.colorInfo`, `token.colorText`, `token.colorTextSecondary`, `token.colorBgContainer`, `token.colorBgLayout`, `token.colorBorder`
- **Spacing**: `token.padding`, `token.paddingSM`, `token.paddingLG`, `token.margin`, `token.marginSM`, `token.marginLG`
- **Borders**: `token.borderRadius`, `token.borderRadiusSM`, `token.borderRadiusLG`
- **Typography**: `token.fontSize`, `token.fontSizeLG`, `token.fontSizeSM`, `token.fontWeightStrong`
- **Shadows**: `token.boxShadow`, `token.boxShadowSecondary`
- **Sizing**: `token.controlHeight`, `token.controlHeightLG`, `token.controlHeightSM`

Then generate the file:

---

### File: `src/components/{feature}/style/{ComponentName}.style.ts`

```typescript
import { createStyles } from 'antd-style';

const useStyles = createStyles(({ token, css }) => ({
  // Classes generated based on user input
  // Examples:

  container: css`
    padding: ${token.paddingLG}px;
    background: ${token.colorBgContainer};
    border-radius: ${token.borderRadiusLG}px;
    border: 1px solid ${token.colorBorder};
  `,

  header: css`
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: ${token.marginLG}px;
  `,

  title: css`
    font-size: ${token.fontSizeLG}px;
    font-weight: ${token.fontWeightStrong};
    color: ${token.colorText};
    margin: 0;
  `,

  // Responsive example:
  // responsiveGrid: css`
  //   display: grid;
  //   grid-template-columns: repeat(4, 1fr);
  //   gap: ${token.margin}px;
  //
  //   @media (max-width: 768px) {
  //     grid-template-columns: repeat(2, 1fr);
  //   }
  //
  //   @media (max-width: 480px) {
  //     grid-template-columns: 1fr;
  //   }
  // `,
}));

export default useStyles;
```

Replace the example classes with real ones based on user input.

---

Usage in the component:
```typescript
import useStyles from './style/{ComponentName}.style';

const {ComponentName} = () => {
  const { styles } = useStyles();

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h2 className={styles.title}>...</h2>
      </div>
    </div>
  );
};
```

---

After generating, remind the user to:
1. Import `useStyles` at the top of the component file
2. Destructure `styles` from the hook: `const { styles } = useStyles();`
3. Apply classes with `className={styles.className}`
4. Never hardcode colors or spacing — always use token values for dark mode compatibility
