# Add List Page

Generate a full list page with filters, a data table, and a lazily-loaded CRUD modal. Follows the pattern: useEffect on filters → fetch → render DataTable + PageHeader + filter controls.

## Arguments

`$ARGUMENTS` — the feature name (e.g. `invoice`, `lead`, `product`). If not provided, ask the user for it.

## Instructions

Parse `$ARGUMENTS` to extract:
- **featureName**: camelCase (e.g. `invoice`)
- **FeatureName**: PascalCase (e.g. `Invoice`)
- **featureNames**: plural camelCase (e.g. `invoices`)
- **FeatureNames**: plural PascalCase (e.g. `Invoices`)

Before generating, ask the user:
1. What filters should the page have? (search bar, status dropdown, date range, etc.)
2. What columns should the table show? (name, date, status, amount, actions, etc.) — enough info to generate `columns.tsx`
3. What actions are available per row? (view detail, edit, delete, approve, etc.)
4. What RBAC roles can create/delete? (e.g. only Admin and Manager can delete)
5. What is the route path for this page? (e.g. `/invoices`)
6. Does each row link to a detail page at `/{featureNames}/[id]`?

Then generate two files:

---

### File 1: `src/app/(main)/{featureNames}/columns.tsx`

```typescript
import Link from 'next/link';
import { Button, Popconfirm, Space, Tag } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import { {FeatureName} } from '@/types';

interface ColumnDeps {
  onEdit: ({featureName}: {FeatureName}) => void;
  onDelete: (id: string) => void;
  canDelete: boolean;
  canEdit: boolean;
}

export const getColumns = ({ onEdit, onDelete, canDelete, canEdit }: ColumnDeps): ColumnsType<{FeatureName}> => [
  {
    title: 'Name',
    dataIndex: 'name',
    key: 'name',
    render: (value: string, record: {FeatureName}) => (
      <Link href={`/{featureNames}/${record.id}`}>{value}</Link>
    ),
  },
  // Additional columns based on user input
  // Status column example:
  // {
  //   title: 'Status',
  //   dataIndex: 'status',
  //   key: 'status',
  //   render: (status: string) => <Tag color={status === 'active' ? 'green' : 'red'}>{status}</Tag>,
  // },
  {
    title: 'Actions',
    key: 'actions',
    width: 150,
    render: (_: unknown, record: {FeatureName}) => (
      <Space>
        {canEdit && (
          <Button size="small" onClick={() => onEdit(record)}>
            Edit
          </Button>
        )}
        {canDelete && (
          <Popconfirm
            title="Delete {featureName}"
            description="Are you sure you want to delete this {featureName}?"
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

---

### File 2: `src/app/(main)/{featureNames}/page.tsx`

```typescript
'use client';

import { useEffect, useState } from 'react';
import dynamic from 'next/dynamic';
import { Input, Select, Button, Row, Col, Space } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { PageHeader } from '@/components/shared/PageHeader';
import { DataTable } from '@/components/shared/DataTable';
import { use{FeatureName}, use{FeatureName}Actions } from '@/providers/{featureName}Provider';
import { useHasRole } from '@/hooks/useHasRole';
import { UserRole } from '@/types/enums';
import { {FeatureName} } from '@/types';
import { getColumns } from './columns';

const {FeatureName}Modal = dynamic(
  () => import('@/components/{featureName}/{FeatureName}Modal'),
  { ssr: false }
);

const {FeatureNames}Page = () => {
  const { {featureNames}, isPending, filters, totalCount } = use{FeatureName}();
  const { fetch{FeatureNames}, delete{FeatureName}, setFilters } = use{FeatureName}Actions();

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selected{FeatureName}, setSelected{FeatureName}] = useState<{FeatureName} | null>(null);

  const canCreate = useHasRole([UserRole.Admin, UserRole.SalesManager]);
  const canEdit = useHasRole([UserRole.Admin, UserRole.SalesManager]);
  const canDelete = useHasRole([UserRole.Admin]);

  useEffect(() => {
    fetch{FeatureNames}();
  }, [filters]);

  const handleEdit = ({featureName}: {FeatureName}) => {
    setSelected{FeatureName}({featureName});
    setIsModalOpen(true);
  };

  const handleDelete = async (id: string) => {
    await delete{FeatureName}(id);
    fetch{FeatureNames}();
  };

  const handleModalClose = () => {
    setIsModalOpen(false);
    setSelected{FeatureName}(null);
  };

  const handleSuccess = () => {
    fetch{FeatureNames}();
  };

  const columns = getColumns({
    onEdit: handleEdit,
    onDelete: handleDelete,
    canEdit,
    canDelete,
  });

  return (
    <>
      <PageHeader
        title="{FeatureNames}"
        breadcrumbs={[{ title: 'Home', href: '/' }, { title: '{FeatureNames}' }]}
        extra={
          canCreate && (
            <Button
              type="primary"
              icon={<PlusOutlined />}
              onClick={() => setIsModalOpen(true)}
            >
              New {FeatureName}
            </Button>
          )
        }
      />

      {/* Filters */}
      <Row gutter={[16, 16]} style={{ marginBottom: 16 }}>
        <Col xs={24} sm={12} md={8}>
          <Input.Search
            placeholder="Search {featureNames}..."
            value={filters.searchTerm}
            onChange={(e) => setFilters({ searchTerm: e.target.value })}
            allowClear
          />
        </Col>
        {/* Additional filter controls based on user input */}
      </Row>

      <DataTable
        columns={columns}
        dataSource={{featureNames}}
        loading={isPending}
        rowKey="id"
        pagination={{
          current: filters.pageNumber,
          pageSize: filters.pageSize,
          total: totalCount,
          onChange: (page, pageSize) => setFilters({ pageNumber: page, pageSize }),
        }}
      />

      <{FeatureName}Modal
        open={isModalOpen}
        onClose={handleModalClose}
        onSuccess={handleSuccess}
        {featureName}={selected{FeatureName}}
      />
    </>
  );
};

export default {FeatureNames}Page;
```

---

After generating, remind the user to:
1. Adjust RBAC roles in the `useHasRole` calls to match the project's role definitions
2. Add any additional filter controls (Select for status, DatePicker for ranges, etc.) based on earlier answers
3. Ensure `{FeatureName}Provider` wraps this page in `src/providers/index.tsx`
4. If a detail page is needed, run `/add-detail-page {featureName}`
