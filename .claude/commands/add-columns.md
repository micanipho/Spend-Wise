# Add Table Columns

Generate a typed `getColumns()` function for an Ant Design Table. Produces a standalone columns file that receives callbacks and permissions as dependencies, keeping column logic out of page components.

## Arguments

`$ARGUMENTS` — the feature name (e.g. `invoice`, `lead`, `product`). If not provided, ask the user for it.

## Instructions

Parse `$ARGUMENTS` to extract:
- **featureName**: camelCase (e.g. `invoice`)
- **FeatureName**: PascalCase (e.g. `Invoice`)
- **featureNames**: plural camelCase (e.g. `invoices`)

Before generating, ask the user:
1. List every column needed: field name, display label, data type (string, number, date, enum/status, boolean), and any special rendering (link to detail page, currency format, colored tag, etc.)
2. What row-level actions are available? (view, edit, delete, approve, complete, etc.)
3. What permissions gate each action? (canEdit, canDelete, canApprove, etc.)
4. Which column links to the detail page?

Then generate the file:

---

### File: `src/app/(main)/{featureNames}/columns.tsx`

```typescript
import Link from 'next/link';
import { Button, Popconfirm, Space, Tag } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import { {FeatureName} } from '@/types';

interface ColumnDeps {
  // Callbacks for each action
  onEdit?: ({featureName}: {FeatureName}) => void;
  onDelete?: (id: string) => void;
  // Permission flags
  canEdit?: boolean;
  canDelete?: boolean;
  // Add other deps based on user input (e.g. theme tokens, user role)
}

export const getColumns = ({
  onEdit,
  onDelete,
  canEdit,
  canDelete,
}: ColumnDeps): ColumnsType<{FeatureName}> => [
  // --- Columns generated based on user input ---

  // Example: Name column with link to detail
  // {
  //   title: 'Name',
  //   dataIndex: 'name',
  //   key: 'name',
  //   sorter: true,
  //   render: (value: string, record: {FeatureName}) => (
  //     <Link href={`/{featureNames}/${record.id}`}>{value}</Link>
  //   ),
  // },

  // Example: Date column with formatting
  // {
  //   title: 'Created',
  //   dataIndex: 'createdAt',
  //   key: 'createdAt',
  //   render: (date: string) => new Date(date).toLocaleDateString(),
  //   sorter: true,
  // },

  // Example: Currency column
  // {
  //   title: 'Amount',
  //   dataIndex: 'amount',
  //   key: 'amount',
  //   align: 'right',
  //   render: (value: number) =>
  //     new Intl.NumberFormat('en-ZA', { style: 'currency', currency: 'ZAR' }).format(value),
  // },

  // Example: Status column with colored tag
  // {
  //   title: 'Status',
  //   dataIndex: 'status',
  //   key: 'status',
  //   render: (status: string) => {
  //     const colorMap: Record<string, string> = {
  //       active: 'green',
  //       inactive: 'red',
  //       pending: 'orange',
  //     };
  //     return <Tag color={colorMap[status] ?? 'default'}>{status}</Tag>;
  //   },
  // },

  // Example: Boolean column
  // {
  //   title: 'Active',
  //   dataIndex: 'isActive',
  //   key: 'isActive',
  //   render: (value: boolean) => <Tag color={value ? 'green' : 'red'}>{value ? 'Active' : 'Inactive'}</Tag>,
  // },

  // Actions column (always last)
  {
    title: 'Actions',
    key: 'actions',
    width: 150,
    fixed: 'right',
    render: (_: unknown, record: {FeatureName}) => (
      <Space size="small">
        {canEdit && onEdit && (
          <Button size="small" onClick={() => onEdit(record)}>
            Edit
          </Button>
        )}
        {canDelete && onDelete && (
          <Popconfirm
            title="Delete {featureName}"
            description="Are you sure? This cannot be undone."
            onConfirm={() => onDelete(record.id)}
            okText="Delete"
            okButtonProps={{ danger: true }}
          >
            <Button size="small" danger>
              Delete
            </Button>
          </Popconfirm>
        )}
      </Space>
    ),
  },
];
```

Replace the example comments with real columns based on user input. Only import Ant Design components that are actually used.

---

After generating, remind the user to:
1. Pass this to `<DataTable columns={columns} />` or Ant Design `<Table columns={columns} />`
2. Import `getColumns` in the page and call it inside `useMemo` if the deps are stable references
3. Run `/add-list-page {featureName}` to generate the full page that uses these columns
