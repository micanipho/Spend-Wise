# Add Detail Page

Generate a full entity detail page with metric cards, tabbed layout, related data tables, and embedded modals for related entity actions.

## Arguments

`$ARGUMENTS` — the feature name (e.g. `invoice`, `lead`, `product`). If not provided, ask the user for it.

## Instructions

Parse `$ARGUMENTS` to extract:
- **featureName**: camelCase (e.g. `invoice`)
- **FeatureName**: PascalCase (e.g. `Invoice`)

Before generating, ask the user:
1. What metric cards should appear at the top? (e.g. total value, status, created date, related counts)
2. What tabs should the page have? (e.g. Overview, Line Items, Activity History, Documents)
3. What related entities are shown in tables within tabs? (e.g. an Invoice detail shows LineItems and Payments)
4. What actions are available on this page? (Edit main entity, Delete, status transitions like Approve/Reject)
5. What RBAC roles can perform each action?
6. Are there any embedded modals for creating/editing related entities inline?

Then generate the file:

---

### File: `src/app/(main)/{featureNames}/[id]/page.tsx`

```typescript
'use client';

import { useEffect, useState, useCallback } from 'react';
import { useParams, useRouter } from 'next/navigation';
import dynamic from 'next/dynamic';
import { Row, Col, Tabs, Descriptions, Table, Button, Popconfirm, Tag, Spin, message } from 'antd';
import { ArrowLeftOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { PageHeader } from '@/components/shared/PageHeader';
import { MetricCard } from '@/components/shared/MetricCard';
import { use{FeatureName}Actions } from '@/providers/{featureName}Provider';
import { useHasRole } from '@/hooks/useHasRole';
import { UserRole } from '@/types/enums';
import { {FeatureName} } from '@/types';

const {FeatureName}Modal = dynamic(
  () => import('@/components/{featureName}/{FeatureName}Modal'),
  { ssr: false }
);

// Add more dynamic modal imports for related entity modals

const {FeatureName}DetailPage = () => {
  const { id } = useParams<{ id: string }>();
  const router = useRouter();
  const { fetch{FeatureName}ById, delete{FeatureName} } = use{FeatureName}Actions();

  const [{featureName}, set{FeatureName}] = useState<{FeatureName} | null>(null);
  const [loading, setLoading] = useState(true);
  // Add state for related entities: const [relatedItems, setRelatedItems] = useState([]);

  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  // Add state for other modals

  const canEdit = useHasRole([UserRole.Admin, UserRole.SalesManager]);
  const canDelete = useHasRole([UserRole.Admin]);

  const loadData = useCallback(async () => {
    if (!id) return;
    setLoading(true);
    try {
      // Use Promise.all for parallel fetching of main entity + related data
      const [main] = await Promise.all([
        fetch{FeatureName}ById(id),
        // fetchRelatedItems(id),
      ]);
      set{FeatureName}(main as unknown as {FeatureName});
      // setRelatedItems(related);
    } catch (error: any) {
      message.error(error?.message ?? 'Failed to load data');
    } finally {
      setLoading(false);
    }
  }, [id]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const handleDelete = async () => {
    if (!id) return;
    try {
      await delete{FeatureName}(id);
      message.success('{FeatureName} deleted successfully');
      router.push('/{featureNames}');
    } catch (error: any) {
      message.error(error?.message ?? 'Failed to delete');
    }
  };

  if (loading) {
    return (
      <div style={{ display: 'flex', justifyContent: 'center', padding: 48 }}>
        <Spin size="large" />
      </div>
    );
  }

  if (!{featureName}) {
    return <div style={{ padding: 24 }}>{FeatureName} not found.</div>;
  }

  const tabItems = [
    {
      key: 'overview',
      label: 'Overview',
      children: (
        <Descriptions bordered column={{ xs: 1, sm: 2 }} size="small">
          {/* Add Descriptions.Item entries for each entity field */}
          {/* <Descriptions.Item label="Name">{featureName}.name</Descriptions.Item> */}
        </Descriptions>
      ),
    },
    // Add more tabs based on user input
    // {
    //   key: 'related',
    //   label: 'Related Items',
    //   children: (
    //     <Table dataSource={relatedItems} columns={relatedColumns} rowKey="id" />
    //   ),
    // },
  ];

  return (
    <>
      <PageHeader
        title={{featureName}.name ?? '{FeatureName} Detail'}
        breadcrumbs={[
          { title: 'Home', href: '/' },
          { title: '{FeatureNames}', href: '/{featureNames}' },
          { title: {featureName}.name ?? id },
        ]}
        extra={
          <Button.Group>
            <Button icon={<ArrowLeftOutlined />} onClick={() => router.back()}>
              Back
            </Button>
            {canEdit && (
              <Button
                type="primary"
                icon={<EditOutlined />}
                onClick={() => setIsEditModalOpen(true)}
              >
                Edit
              </Button>
            )}
            {canDelete && (
              <Popconfirm
                title="Delete {FeatureName}"
                description="This action cannot be undone."
                onConfirm={handleDelete}
                okText="Delete"
                okButtonProps={{ danger: true }}
              >
                <Button danger icon={<DeleteOutlined />}>
                  Delete
                </Button>
              </Popconfirm>
            )}
          </Button.Group>
        }
      />

      {/* Metric Cards */}
      <Row gutter={[16, 16]} style={{ marginBottom: 24 }}>
        {/* Add MetricCard components based on user-specified metrics */}
        {/* <Col xs={24} sm={12} md={6}>
          <MetricCard title="Status" value={{featureName}.status} />
        </Col> */}
      </Row>

      <Tabs items={tabItems} />

      <{FeatureName}Modal
        open={isEditModalOpen}
        onClose={() => setIsEditModalOpen(false)}
        onSuccess={loadData}
        {featureName}={{featureName}}
      />

      {/* Add other modals for related entity creation/editing */}
    </>
  );
};

export default {FeatureName}DetailPage;
```

---

After generating, remind the user to:
1. Fill in Descriptions.Item entries for each field of the entity
2. Add MetricCard rows with meaningful KPIs
3. Add tab content for each related entity table (with its own column definitions inline or in a separate file)
4. Adjust RBAC roles to match the project's actual role definitions
5. Run `/add-modal {featureName}` for the edit modal if it doesn't exist yet
